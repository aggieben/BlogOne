# CoffeeScript
class Popover
    @_current = null
    @_removeTrigger: (e) ->    
        pop = Popover._current
        if pop? 
            if pop.firstTime
                pop.firstTime = false
                # check for autofocus
                afn = document.querySelector('*[autofocus="true"]')
                afn.focus() if pop.contains afn
            else if e.which not in [16, 17, 18, 91] # ctrl, alt, shift, cmd
                pop.remove()

    constructor: (opts) ->
        @firstTime = true
        div = document.createElement 'div'
        div.setAttribute('class','bo-popover')
        div.style.visibility = 'hidden'
        div.innerHTML = opts.content ? ''
        document.body.appendChild(div)
        
        switch opts.position
            when 'selection'
                coords = @_getSelectionCoords()
                console.log(coords)
                div.style.left = "#{coords.x}px"
                div.style.top = "#{coords.y}px"
                div.style.marginTop = "-#{Math.floor(10+div.clientHeight)}px"
                div.style.marginLeft = "-#{Math.floor(div.clientWidth/2)}px"
                div.style.visibility = 'visible'
                
        @div = div
        Popover._current?.remove()
        Popover._current = @
        # *sigh*.  I was hoping to avoid dependencies of any kind, but events are a pain without jquery.
        # if someone else uses this someday and it's a big deal, he can file an issue on GitHub.
        $(div).on 'click keyup', (e) -> false unless e.keyCode is 27 # esc
        $(document).on 'click keyup', Popover._removeTrigger
                
                
    remove: () ->
        @div.parentNode.removeChild(@div)    
        Popover._current = null
        $(document).off 'click keyup', Popover._removeTrigger
        
    contains: (node) ->
        @div.contains(node)
        
    
        
    _getSelectionCoords: () ->
        sel = document.selection
        if sel? and sel.type isnt 'Control'
            range = sel.createRange() 
            rect = range.getClientRects()[0]
            [x, y] = [range.boundingLeft + (rect.width / 2), range.boundingTop]
        else if window.getSelection?
            sel = window.getSelection()
            if sel.rangeCount?
                range = sel.getRangeAt(0).cloneRange()
                if range.getClientRects?
                    rect = range.getClientRects()[0]
                    [x,y] = [rect.left + (rect.width / 2), rect.top]
        @range = range
        return { x: x, y: y}