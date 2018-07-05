//# sourceURL=wisej.web.ext.xTerm.startup.js

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

	var me = this;
    options = $options;

	if (wisej.web.DesignMode) {
		options.autoLoad = false;
	}

    Terminal.applyAddon(attach);
    Terminal.applyAddon(fit);
    Terminal.applyAddon(fullscreen);
    Terminal.applyAddon(search);
    Terminal.applyAddon(webLinks);
    Terminal.applyAddon(winptyCompat);

    this.term = new Terminal({
        cursorBlink: true,
        scrollback: 1000,
        tabStopWith: 4
    });

    this.term.current = 0;
    this.term.x = 0;
    this.term.setOption('debug', options.debugScript);
    this.term.open(this.container);
    this.term.winptyCompatInit();
    this.term.webLinksInit();
    this.term.fit();
    this.term.focus()
    this.term.on('key', (key, ev) => {
        var printable = (
            !ev.altKey && !ev.altGraphKey && !ev.ctrlKey && !ev.metaKey
        );

        if (ev.keyCode == 8) {
            // Do not delete the prompt
            if (this.term.current > this.term.x) {
                this.term.write('\b \b');
                this.term.current--;
                me.fireWidgetEvent('key', key);
            } 
        }
        else if (key.charCodeAt(0) == 13) {
            //this.term.write('\n');
            this.term.x = this.term.current;
            me.fireWidgetEvent('key', key);
        }
        else if (printable) {
            this.term.write(key);
            this.term.current++;
            me.fireWidgetEvent('key', key);
        }
    });
    this.term.on('open', function () {
        alert('opened');
    });
    this.term.on('paste', function (data, ev) {
        term.write(data);
    });
    me.fireWidgetEvent('init', 'init');
    //window.term = this.term;
};

this.termWrite = function (message) {
    this.term.write(message);
    this.term.x += message.length;
    this.term.current += message.length;
}

/**
 * Called when the options change. It lets the 
 * widget decide whether to update an existing
 * third-party control or to create a new one.
 */
this.update = function (options) {

	// recreate the viewer.
	this.init(options);
}