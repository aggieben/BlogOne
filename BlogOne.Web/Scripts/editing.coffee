# CoffeeScript
do ($ = jQuery) ->
    _rvt = $('article.content input[name="__RequestVerificationToken"]').val()
    _pid = parseInt($('article.content').data('post-id'), 10)
    
    publish = () ->
        $.ajax "/post/#{_pid}/publish", 
            type: 'POST'
            data: 
                __RequestVerificationToken: _rvt
            error: (jqxhr, status, error) ->
                console.log error
                console.log status
                console.log jqxhr
            success: (data, status, jqxhr) ->
                console.log 'publish succeeded'
        
    submit = () ->
        console.log 'submit'
        
    share = () ->
        console.log 'share'
        
    remove = () ->
        console.log 'delete'
        
    $('button.publish').on 'click', (e) ->
        publish()
    $('button.submit').on 'click', (e) ->
        submit()
    $('button.share').on 'click', (e) ->
        share()
    $('button.delete').on 'click', (e) ->
        remove()