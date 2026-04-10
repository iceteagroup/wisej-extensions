(function (root, factory) {
  if (typeof define === "function" && define.amd) {
    // AMD
    define([], factory);
  } else if (typeof module === "object" && module.exports) {
    // Node / CommonJS
    module.exports = factory();
  } else {
    // Browser global
    root.EasingEffects = factory();
  }
}(this, function () {
  "use strict";

  var PI = Math.PI;
  var TAU = 2 * Math.PI;
  var HALF_PI = Math.PI / 2;

  function atEdge(t) {
    return t === 0 || t === 1;
  }

  function elasticIn(t, s, p) {
    t -= 1;
    return -(Math.pow(2, 10 * t) * Math.sin((t - s) * TAU / p));
  }

  function elasticOut(t, s, p) {
    return Math.pow(2, -10 * t) * Math.sin((t - s) * TAU / p) + 1;
  }

  var effects = {
    linear: function (t) { return t; },

    easeInQuad: function (t) { return t * t; },

    easeOutQuad: function (t) { return -t * (t - 2); },

    easeInOutQuad: function (t) {
      t /= 0.5;
      return (t < 1) ? 0.5 * t * t : -0.5 * ((--t) * (t - 2) - 1);
    },

    easeInCubic: function (t) { return t * t * t; },

    easeOutCubic: function (t) { t -= 1; return t * t * t + 1; },

    easeInOutCubic: function (t) {
      t /= 0.5;
      return (t < 1) ? 0.5 * t * t * t : 0.5 * ((t -= 2) * t * t + 2);
    },

    easeInQuart: function (t) { return t * t * t * t; },

    easeOutQuart: function (t) { t -= 1; return -(t * t * t * t - 1); },

    easeInOutQuart: function (t) {
      t /= 0.5;
      return (t < 1) ? 0.5 * t * t * t * t : -0.5 * ((t -= 2) * t * t * t - 2);
    },

    easeInQuint: function (t) { return t * t * t * t * t; },

    easeOutQuint: function (t) { t -= 1; return t * t * t * t * t + 1; },

    easeInOutQuint: function (t) {
      t /= 0.5;
      return (t < 1)
        ? 0.5 * t * t * t * t * t
        : 0.5 * ((t -= 2) * t * t * t * t + 2);
    },

    easeInSine: function (t) { return -Math.cos(t * HALF_PI) + 1; },

    easeOutSine: function (t) { return Math.sin(t * HALF_PI); },

    easeInOutSine: function (t) { return -0.5 * (Math.cos(PI * t) - 1); },

    easeInExpo: function (t) { return (t === 0) ? 0 : Math.pow(2, 10 * (t - 1)); },

    easeOutExpo: function (t) { return (t === 1) ? 1 : -Math.pow(2, -10 * t) + 1; },

    easeInOutExpo: function (t) {
      if (atEdge(t)) return t;
      return t < 0.5
        ? 0.5 * Math.pow(2, 10 * (t * 2 - 1))
        : 0.5 * (-Math.pow(2, -10 * (t * 2 - 1)) + 2);
    },

    easeInCirc: function (t) { return (t >= 1) ? t : -(Math.sqrt(1 - t * t) - 1); },

    easeOutCirc: function (t) { t -= 1; return Math.sqrt(1 - t * t); },

    easeInOutCirc: function (t) {
      t /= 0.5;
      return (t < 1)
        ? -0.5 * (Math.sqrt(1 - t * t) - 1)
        : 0.5 * (Math.sqrt(1 - (t -= 2) * t) + 1);
    },

    easeInElastic: function (t) {
      return atEdge(t) ? t : elasticIn(t, 0.075, 0.3);
    },

    easeOutElastic: function (t) {
      return atEdge(t) ? t : elasticOut(t, 0.075, 0.3);
    },

    easeInOutElastic: function (t) {
      var s = 0.1125;
      var p = 0.45;
      if (atEdge(t)) return t;
      return (t < 0.5)
        ? 0.5 * elasticIn(t * 2, s, p)
        : 0.5 + 0.5 * elasticOut(t * 2 - 1, s, p);
    },

    easeInBack: function (t) {
      var s = 1.70158;
      return t * t * ((s + 1) * t - s);
    },

    easeOutBack: function (t) {
      var s = 1.70158;
      t -= 1;
      return t * t * ((s + 1) * t + s) + 1;
    },

    easeInOutBack: function (t) {
      var s = 1.70158;
      t /= 0.5;
      if (t < 1) {
        return 0.5 * (t * t * (((s *= 1.525) + 1) * t - s));
      }
      t -= 2;
      return 0.5 * (t * t * (((s *= 1.525) + 1) * t + s) + 2);
    },

    easeInBounce: function (t) {
      return 1 - effects.easeOutBounce(1 - t);
    },

    easeOutBounce: function (t) {
      var m = 7.5625;
      var d = 2.75;
      if (t < (1 / d)) {
        return m * t * t;
      }
      if (t < (2 / d)) {
        t -= (1.5 / d);
        return m * t * t + 0.75;
      }
      if (t < (2.5 / d)) {
        t -= (2.25 / d);
        return m * t * t + 0.9375;
      }
      t -= (2.625 / d);
      return m * t * t + 0.984375;
    },

    easeInOutBounce: function (t) {
      return (t < 0.5)
        ? effects.easeInBounce(t * 2) * 0.5
        : effects.easeOutBounce(t * 2 - 1) * 0.5 + 0.5;
    }
  };

  return effects;
}));
