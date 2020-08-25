var titleElement = document.getElementById("timer-title");
var finishTimeElement = document.getElementById("finish-time-input");

var countDownDate;
if (finishTimeElement) {
    countDownDate = parseInt(finishTimeElement.value);
}

updateLabel();

if (titleElement && countDownDate) {
    var x = setInterval(updateLabel, 1000);
}

function updateLabel() {
    if (titleElement) {
        var now = new Date().getTime();
        var timeLeft = countDownDate - now;
        var minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);

        if (timeLeft <= 0) {
            clearInterval(x);
            window.location.replace("/Game/TimerIsUp");
        } else if (minutes > 0) {
            titleElement.innerHTML = minutes + "m " + seconds + "s";
        } else {
            titleElement.innerHTML = seconds + "s";
        }
    }
}