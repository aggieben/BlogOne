# CoffeeScript
class Popover
    @_current = null

    constructor: (opts) ->
        div = document.createElement 'div'
        div.setAttribute('class','bo-popover')
        div.innerHTML = opts.content ? ''
        document.body.appendChild(div)
        
        switch opts.position
            when 'selection'
                coords = @_getSelectionCoords()
                div.style.left = "#{coords.x}px"
                div.style.top = "#{coords.y}px"
                div.style.marginTop = "-#{Math.floor(10+div.clientHeight)}px"
                div.style.marginLeft = "-#{Math.floor(div.clientWidth/2)}px"
        
        # *sigh*.  I was hoping to avoid dependencies of any kind, but events are a pain without jquery.
        # if someone else uses this someday and it's a big deal, they can file an issue on GitHub.
        $(div).on 'click keyup', (e) -> false unless e.keyCode is 27
            
        @div = div
        unless Popover._current?
            Popover._current = this
            $(document).on 'click keyup', (e) ->
                console.log(e)
                Popover._current?.remove()
                Popover._current = null
                
    remove: () ->
        @div.parentNode.removeChild(@div)     
        
    contains: (node) ->
        @div.contains(node)
        
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