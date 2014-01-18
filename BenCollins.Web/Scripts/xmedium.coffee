# CoffeeScript
class XMedium
    constructor: () ->
    
    @_pops: []    
    @modifiers:
        linkify: (e) ->
            $ae = $(document.activeElement)
            $ae.popover
                trigger: 'manual'
                placement: 'auto'
                title: 'Link URL'
                html: true
                content: """
                         <input type="url" />
                         """
                template: """
                            <div class="popover" style="">
                                <div class="arrow"></div>
                                <h3 class="popover-title"></h3>
                                <div class="popover-content"></div>
                            </div>
                          """
            # need to get current selection coordinates to reposition the popover: 
            # http://stackoverflow.com/questions/6846230/javascript-text-selection-page-coordinates
            $ae.popover('show')
            XMedium._pops.push $ae
            
    @clearPopovers: () ->
        elem.popover 'destroy' for elem in XMedium._pops
