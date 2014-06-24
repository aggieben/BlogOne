# CoffeeScript
class XMedium
    constructor: () ->
      
    @modifiers:
        linkify: (e) ->
            eventFilter = (e) -> (e.key? and e.key is 'enter') or (e.which? and e.which is 13)
            eventHandler = (e) -> document.execCommand('createLink', false, e.target.value)
            pop = new Popover
                position: 'selection'
                content: """
                            <input type="url" size="35" autofocus="true" placeholder="paste or type a url"/>
                         """
                eventing: [ {selector: 'input', event: 'keyup', filter: eventFilter, handler: eventHandler} ]
        
        codefmt: (e) ->
            range = window.getSelection().getRangeAt(0)
            pre = document.createElement('pre')
            code = document.createElement('code')
            range.surroundContents(code)
            
            pre.appendChild(code.parentNode.replaceChild(pre, code))
        
        gistins: (e) ->
            eventFilter = (e) -> (e.key? and e.key is 'enter') or (e.which? and e.which is 13)
            eventHandler = (e) -> console.log('submit gist')
            pop = new Popover
                postion: 'selection'
                content: """
                            <input size="35" autofocus="true" placeholder="paste or type a gist id or url"/>
                         """
                eventing: [ {selector: 'input', event: 'keyup', filter: eventFilter, handler: eventHandler} ]
