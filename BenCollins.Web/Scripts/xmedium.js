﻿// Generated by CoffeeScript 1.6.3
var XMedium;

XMedium = (function() {
  function XMedium() {}

  XMedium._pops = [];

  XMedium.modifiers = {
    linkify: function(e) {
      var $ae;
      $ae = $(document.activeElement);
      $ae.popover({
        trigger: 'manual',
        placement: 'auto',
        title: 'Link URL',
        html: true,
        content: "<input type=\"url\" />",
        template: "<div class=\"popover\" style=\"\">\n    <div class=\"arrow\"></div>\n    <h3 class=\"popover-title\"></h3>\n    <div class=\"popover-content\"></div>\n</div>"
      });
      $ae.popover('show');
      return XMedium._pops.push($ae);
    }
  };

  XMedium.clearPopovers = function() {
    var elem, _i, _len, _ref, _results;
    _ref = XMedium._pops;
    _results = [];
    for (_i = 0, _len = _ref.length; _i < _len; _i++) {
      elem = _ref[_i];
      _results.push(elem.popover('destroy'));
    }
    return _results;
  };

  return XMedium;

})();