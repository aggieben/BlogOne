# CoffeeScript
do ($ = jQuery) ->
    $('.profile').on 'click', (e) -> 
        $topbar = $('.top-bar')
        targetMargin = switch
            when (parseInt $topbar.css('margin-top')) < 0 then 0
            else -$topbar.height()
        $topbar.animate({ "margin-top": targetMargin })