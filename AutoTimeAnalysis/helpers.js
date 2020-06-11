//All elements with a scroll
var ElementsWithScrolls = (function() {
    var getComputedStyle = document.body && document.body.currentStyle ? function(elem) {
        return elem.currentStyle;
    } : function(elem) {
        return document.defaultView.getComputedStyle(elem, null);
    };
  
    function getActualCss(elem, style) {
        return getComputedStyle(elem)[style];
    }
  
    function isXScrollable(elem) {
        return elem.offsetWidth < elem.scrollWidth &&
            autoOrScroll(getActualCss(elem, 'overflow-x'));
    }
  
    function isYScrollable(elem) {
        return elem.offsetHeight < elem.scrollHeight &&
            autoOrScroll(getActualCss(elem, 'overflow-y'));
    }
  
    function autoOrScroll(text) {
        return text == 'scroll' || text == 'auto';
    }
  
    function hasScroller(elem) {
        return isYScrollable(elem) || isXScrollable(elem);
    }
    return function ElemenetsWithScrolls() {
        return [].filter.call(document.querySelectorAll('*'), hasScroller);
    };
  })();
  