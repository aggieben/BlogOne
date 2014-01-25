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
                window.location.pathname = "/post/edit/#{data}"
            
            
    last = -1
    $('article.content section.editable').on 'input', (e) ->
        console.log 'saving draft...'
        clearTimeout last
        last = setTimeout (() -> save()), 500
        
    
            
        
    