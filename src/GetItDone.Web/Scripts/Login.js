


function verifyTokenAndRedirect(){
    if(localStorage.trello_token)
    {
        $('form').attr("action", "Login?token=" + localStorage.trello_token)
        $('form').submit();
    }
}

$(function () {
    $("#loginTag").click(function () {
        Trello.authorize({
            type: 'redirect',
            name: 'My App',
            persist: 'true',
            scope: {
                read: true, write: true, account: true
            },
            expiration: 'never',
            success: verifyTokenAndRedirect
        });
    })
    verifyTokenAndRedirect();

    
});

