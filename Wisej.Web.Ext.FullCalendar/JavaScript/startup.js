//# sourceURL=wisej.web.ext.FullCalendar.startup.js

/**
 * Initializes the widget.
 *
 * This function is called when the InitScript property of
 * wisej.web.Widget changes.
 *
 * "this" refers to the container which is a wisej.web.Widget instance.
 *
 * The widget has an inner container with id = "container" that can
 * be used referring to this.container.
 *
 */
this.init = function (options) {

	// prepare the configuration map.
	// [$]options is a placeholder that is replaced with the options
	// map configured in Wisej.Web.Ext.FullCalendar.
	options = options || {};
	qx.lang.Object.mergeWith(options, $options, true);
	options.header = false;
	options.timezone = "local";
	options.height = this.getHeight();

	if (qx.core.Environment.get("qx.rtl.supported"))
		options.isRTL = this.getRtl() == true;

	//  translate our themed colors.
	this.__colorMgr = qx.theme.manager.Color.getInstance();
	this.__resolveColors(options);

	if (!wisej.web.DesignMode) {

		// attach our event handler to fire the event back to the server.
		options.events = this.loadEvents.bind(this);
		options.dayClick = this.onDayClick.bind(this);
		options.eventDrop = this.onEventDrop.bind(this);
		options.eventClick = this.onEventClick.bind(this);
		options.eventResize = this.onEventResize.bind(this);

		// in case the widget is not visible, wait for the "appear" event and render again.
		if (this.calendar == null)
			this.addListener("appear", function (e) { this.calendar.render(); }, this);
	}

	// use our localized day and month names.
	var locale = qx.locale.Manager.getInstance().getLocale();
	options.dayNames = qx.util.Serializer.toNativeObject(qx.locale.Date.getDayNames("wide", locale));
	options.dayNamesShort = qx.util.Serializer.toNativeObject(qx.locale.Date.getDayNames("abbreviated", locale));
	options.monthNames = qx.util.Serializer.toNativeObject(qx.locale.Date.getMonthNames("wide", locale));
	options.monthNamesShort = qx.util.Serializer.toNativeObject(qx.locale.Date.getMonthNames("abbreviated", locale));

	// first creation?
	if (this.calendar == null) {

		var me = this;

		// attach to "resize" to autoresize the calendar to fill the widget container.
		this.addListener("resize", function (e) {
			if (me.calendar) {
				var size = e.getData();
				me.calendar.option("height", size.height);
			}
		});

		// attach to "drop" to handle objects dropped on the calendar.
		this.addListener("drop", this.onItemDrop);

		// attach to "contextmenu" to generate right DayClick events.
		this.addListener("contextmenu", this.onRightClick);

		// rightToLeft support.
		this.addListener("changeRtl", function (e) {

			if (!qx.core.Environment.get("qx.rtl.supported"))
				return;
			var rtl = e.getData();
			if (rtl != null)
				this.calendar.option("isRTL", rtl);

		}, this);

		// create the data store that will retrieve the
		// events from the widget using the IWisejDataStore implementation.
		if (!wisej.web.DesignMode)
			this.__dataStore = new wisej.DataStore(this.getId());
	}

	// destroy the previous FullCalendar instance and recreate it with the new options.
	if (this.calendar != null) {
		this.calendar.destroy();
	}

	// patch the fullCalendar class to handle right clicks.
	this.__patchHandlers();

	this.calendar = new $.fullCalendar.Calendar($(this.container), options);
	this.calendar.render();

	// save the current date in order to fire "currentDateChanged".
	this.__currentDate = this.calendar.getDate();

	// notify that the calendar is created.
	this.fireEvent("initialized");
}

/**
 * Release related objects.
 */
this._onDestroyed = function()
{
	this._disposeObjects("__dataStore");
}

/**
 * Patches the EventPointing.prototype.bindToEl method to add support right clicks.
 */
this.__patchHandlers = function () {

	try {
		$.fullCalendar.EventPointing.prototype.bindToEl = function (el) {
			var component = this.component;
			component.bindSegHandlerToEl(el, 'click', this.handleClick.bind(this));
			component.bindSegHandlerToEl(el, 'contextmenu', this.handleClick.bind(this));
			component.bindSegHandlerToEl(el, 'mouseenter', this.handleMouseover.bind(this));
			component.bindSegHandlerToEl(el, 'mouseleave', this.handleMouseout.bind(this));
		}
	}
	catch (ex) { }
}

/**
 * Executes the specified method with the specified arguments on the calendar widget.
 */
this.exec = function () {

	// defer if the calendar is not created yet.
	if (!this.calendar)
	{
		var args = arguments;
		this.addListenerOnce("initialized", function (e) {
			this.exec.apply(this, args);
		});
		return;
	}

	var name = arguments[0];
	var func = this.calendar[name];
	if (func instanceof Function) {

		if (arguments.length == 1) {
			func.call(this.calendar);
		}
		else {
			func.apply(this.calendar, Array.prototype.slice.call(arguments, 1));
		}

		if (this.__currentDate != this.calendar.getDate())
		{
			this.__currentDate = this.calendar.getDate();
			this.fireWidgetEvent("currentDateChanged", this.__currentDate.toDate());
		}
	}
	else {
		Wisej.Core.logError("FullCalendar", "Method not found:", name);
	}
}

/**
 * Resolves themed colors recursively.
 */
this.__resolveColors = function (data) {

	if (data == null || data instanceof Function)
		return;

	if (data instanceof Array) {
		for (var i = 0; i < data.length; i++) {
			this.__resolveColors(data[i]);
		}
		return;
	}

	var colorMgr = this.__colorMgr;

	for (var name in data) {

		// it's a color, resolve it.
		if (qx.lang.String.endsWith(name, "Color")) {

			data[name] = colorMgr.resolve(data[name]);
			continue;
		}

		// it's an array, go through all elements.
		var array = data[name];
		if (array instanceof Array && array.length > 0) {
			for (var i = 0; i < array.length; i++) {
				this.__resolveColors(array[i]);
			}

			continue;
		}

		// re-enter for child objects.
		var object = data[name];
		if (object instanceof Object) {
			this.__resolveColors(object);
		}
	}
}

/**
 * Re-fetches the current view's events from all sources.
 */
this.refetchEvents = function () {

	if (this.calendar)
		this.calendar.refetchEvents();
}

/**
 * Loads the events from the server.
 */
this.loadEvents = function (start, end, timezone, callback) {

	var args = {
		start: start.local().toDate(),
		end: end.local().toDate()
	};

	this.__dataStore.getDataRows(args, function (data) {

		data = data || [];
		this.__resolveColors(data);
		callback(data);

	}, this);

}

/**
 * Fires "eventDrop" when an event is dragged from one location to another.
 */
this.onEventDrop = function (event, delta) {

	var allDay = !event.start.hasTime();

	if (!allDay && !event.end) {
		var duration = moment.duration(this.calendar.options.defaultTimedEventDuration);
		event.end = event.start.clone().add(duration);
	}

	var start = event.start.local().toDate();
	var end = allDay ? null : event.end.local().toDate();

	this.fireWidgetEvent("eventDrop", {
		id: event.id,
		start: start,
		end: end,
		allDay: allDay,
		resourceId: event.resourceId
	});
}

/**
 * Fires "eventResize" when the duration of an event is changed.
 */
this.onEventResize = function (event) {

	var allDay = !event.start.hasTime();

	var start = event.start.local().toDate();
	var end = allDay ? null : event.end.local().toDate();

	this.fireWidgetEvent("eventResize", {
		id: event.id,
		start: start,
		end: end,
	});

}

// timer to detect single or double clicks.
this.__singleClickTimer = 0;

/**
 * Fires "eventClick" or "eventDblClick" when an event is clicked.
 */
this.onEventClick = function (calEvent, ev, view) {

	var me = this;
	var type = "eventClick";
	var data = {
		id: calEvent.id,
		button: ev.button, x: ev.pageX, y: ev.pageY
	};

	// stop propagation or we may get a second right-click.
	if (ev.button == 2) {
		ev.preventDefault();
		ev.stopPropagation();
	}

	// detect double clicks.
	clearTimeout(this.__singleClickTimer);
	if (this.__singleClickTimer > 0) {

		type = "eventDblClick";
		me.__singleClickTimer = 0;
		me.fireWidgetEvent(type, data);
	}
	else {

		this.__singleClickTimer = setTimeout(function () {

			me.__singleClickTimer = 0;
			me.fireWidgetEvent(type, data);

		}, 250);
	}
}

/**
 * Fires "dayClick" when a user clicks on a day.
 */
this.onDayClick = function (date, ev, view) {

	var me = this;
	var type = "dayClick";
	var day = date.local().toDate();
	if (!date.hasTime())
		day = new Date(date.year(), date.month(), date.date());
	var data = {
		date: day,
		button: ev.button, x: ev.pageX, y: ev.pageY
	};

	// detect double clicks.
	clearTimeout(this.__singleClickTimer);
	if (this.__singleClickTimer > 0) {

		type = "dayDblClick";
		me.__singleClickTimer = 0;
		me.fireWidgetEvent(type, data);
	}
	else {

		this.__singleClickTimer = setTimeout(function () {

			me.__singleClickTimer = 0;
			me.fireWidgetEvent(type, data);

		}, 250);
	}
}

/**
 * Fires "itemDrop" when a wisej widget is dropped on  the calendar.
 */
this.onItemDrop = function (e) {

	// determine the time of the drop.
	var x = e.getDocumentLeft();
	var y = e.getDocumentTop();
	var view = this.calendar.view;
	try {

		view.prepareHits();
		var hit = view.queryHit(x, y);
		view.releaseHits();

		var footprint = hit.component.getSafeHitFootprint(hit);
		var date = this.calendar.footprintToDateProfile(footprint).start;
		var day = date.toDate();
		if (!date.hasTime())
			day = new Date(date.year(), date.month(), date.date());

		this.fireWidgetEvent("itemDrop", {
			date: day,
			x: x, y: y,
			target: e.getDragTarget()
		});
	} catch (ex) {

		Wisej.Core.logError(ex);
	}
}

/**
 * Fires "dayClick" on right clicks (contextmenu) events on a day cell.
 */
this.onRightClick = function (e) {

	// determine the time of the drop.
	var x = e.getDocumentLeft();
	var y = e.getDocumentTop();
	var view = this.calendar.view;
	try {

		view.prepareHits();
		var hit = view.queryHit(x, y);
		view.releaseHits();

		var footprint = hit.component.getSafeHitFootprint(hit);
		var date = this.calendar.footprintToDateProfile(footprint).start;
		var day = date.toDate();
		if (!date.hasTime())
			day = new Date(date.year(), date.month(), date.date());

		this.fireWidgetEvent("dayClick", {
			date: day,
			button: 2,
			x: x, y: y
		});

	} catch (ex) {

		Wisej.Core.logError(ex);
	}
}
