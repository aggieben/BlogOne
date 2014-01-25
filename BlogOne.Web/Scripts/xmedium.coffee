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
                            <input type="url" size="35" autofocus="true" placeholder="paste or type a link"/>
                         """
                eventing: [ {selector: 'input', event: 'keyup', filter: eventFilter, handler: eventHandler} ]
    
