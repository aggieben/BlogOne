# CoffeeScript
class XMedium
    constructor: () ->
      
    @modifiers:
        linkify: (e) ->
            pop = new Popover
                position: 'selection'
                content: '<input type="url" size="35" autofocus="true" placeholder="paste or type a link"/>'
    
    
