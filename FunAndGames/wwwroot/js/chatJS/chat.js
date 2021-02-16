
var currentuseridx = $('#xx').text();
var usernamex = $('#yy').text();



//SLANJE PORUKE
$('.chatWindowContainer').on('keyup', '.messageToSend', function (e) {
    if (e.keyCode === 13) { //if pressed key IS ENTER

        var textInput = $(this);
        var chatwindowdata1 = $(this).parent().parent().attr('data-0');
        var chatwindowdata2 = $(this).parent().parent().attr('data-1');

        //netko šalje poruku drugoj osobi, provjeri jel ta osoba ima otvoren chat prozor
        openChatWindowToHub(chatwindowdata1, chatwindowdata2, usernamex);

        $.ajax({
            url: 'https://localhost:44391/Home/SendMessage',
            method: 'POST',
            data: {
                currentuserid: currentuseridx,
                username: usernamex,
                message: textInput.val()
            },
            success: function () {
                sendMessageToHub(usernamex, textInput.val(), chatwindowdata1, chatwindowdata2);
                textInput.val('');
            },

            error: function () {
                alert('error')
            }
        });
        
    }
});


function addMessageToChat(username, text, chatwindowdata1, chatwindowdata2) {
    
    var newDiv = document.createElement('div');
    newDiv.innerHTML = username + ': ' + text;
    //DODATI NOVU PORUKU U ONAJ CHAT PO ID-U
    var chatWindows = document.getElementsByClassName('chatWindow');
    for (i = 0; i < chatWindows.length; i++)
    {
        //refaktorirat if
        if (($(chatWindows[i]).attr(('data-0')) == chatwindowdata1 || $(chatWindows[i]).attr(('data-0')) == chatwindowdata2)
            && ($(chatWindows[i]).attr(('data-1')) == chatwindowdata1 || $(chatWindows[i]).attr(('data-1')) == chatwindowdata2)) {

            chatWindows[i].childNodes[1].appendChild(newDiv);
        }
    }
}

//ZATVORI PROZOR
$('.chatWindowContainer').on('click', '.chatWindowHeaderCloseChat', function () {
    $(this).parent().parent().remove();
});


