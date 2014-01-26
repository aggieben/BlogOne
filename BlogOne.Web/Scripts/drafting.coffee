# CoffeeScript
do ($ = jQuery) ->
    save = () ->
        content = 
            __RequestVerificationToken: $('article.content input[name="__RequestVerificationToken"]').val()
            title: $('article.content .title :not(.Medium-placeholder)').html()
            subtitle: $('article.content .subtitle :not(.Medium-placeholder)').html()
            body: $('article.content section.editable:not(.Medium-placeholder)').html()
                
        if (pid = $('article.content').data('post-id'))
            content.postId = pid
        
        $.ajax '/post/draft', 
            type: 'POST'
            data: content
            error: (jqxhr, status, error) ->
                console.log error
                console.log status
                console.log jqxhr
            success: (data, status, jqxhr) ->
                window.location.pathname = "/post/edit/#{data}" unless pid?
            
    last = -1
    draftOnEvent = (selector, event, options) ->
        delay = options?.delay ? 750
        condition = options?.condition
        $(selector).on event, (e) ->
            console.log e
            if not condition? or condition() is true
                clearTimeout last
                last = setTimeout (() -> save()), delay
    
    draftOnEvent 'article.content section.editable', 'input'
    draftOnEvent 'article.content .title.editable', 'blur', 
        condition: () -> $('article.content .title .Medium-placeholder').length is 0 
    draftOnEvent 'article.content .subtitle.editable', 'blur', 
        condition: () -> $('article.content .subtitle .Medium-placeholder').length is 0

    
    $('article.content .title').on 'blur', (e) ->
        console.log "title blur: #{@}"
        if $('article.content .title :not(.Medium-placeholder)').length is 0
            clearTimeout last
            last = setTimeout (() -> save()), 750 
            
    
        
    
            
        
    