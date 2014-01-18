# CoffeeScript
class Popover
    constructor: (opts) ->
        div = document.createElement 'div'
        div.setAttribute('class','bo-popover')
 
        switch opts.position
            when 'selection'
                coords = @_getSelectionCoords()
                div.setAttribute('style', "position: absolute; 
                                           left: #{coords.x}px; 
                                           top: #{coords.y}px;
                                           height: 50px;
                                           width: 50px;
                                           background-color: red;")
        document.body.appendChild(div)
        
    _getSelectionElement: () ->
        sel = document.selection
        unless sel
            sel = window.getSelection()
        
        sel.anchorNode.parentElement
    
    _getCoordsRelativeToElement: (element, coords) ->
        
    
    # by Tim Down,  http://stackoverflow.com/a/6847328/3279 License: CC-SA
    _getSelectionCoords: `function () {
        var sel = document.selection, range;
        var x = 0, y = 0;
        if (sel) {
            if (sel.type != "Control") {
                range = sel.createRange();
                var rect = range.getClientRects()[0];
                console.log("rect: ")
                console.log(rect)
                /*range.collapse(true);*/

                x = range.boundingLeft + (rect.width / 2);
                y = range.boundingTop;
            }
        } else if (window.getSelection) {
            sel = window.getSelection();
            if (sel.rangeCount) {
                range = sel.getRangeAt(0).cloneRange();
                if (range.getClientRects) {
                    var rect = range.getClientRects()[0];
                    console.log("rect: ")
                    console.log(rect)
                    /*range.collapse(true);*/

                    x = rect.left + (rect.width / 2);
                    y = rect.top;
                }
            }
        }
        return { x: x, y: y };
    }`