var rect = document.querySelector("rect");
var perimeter = (rect.width.baseVal.value + rect.height.baseVal.value) * 2;
rect.style.strokeDasharray = `${perimeter}`;

var startTimeElement = document.getElementById("start-time-input");
var finishTimeElement = document.getElementById("finish-time-input");

function getPercentLeft() {
    if (startTimeElement && finishTimeElement) {
        var now = new Date().getTime();
        const totalTime = parseInt(finishTimeElement.value) - parseInt(startTimeElement.value);
        const timeLeft = parseInt(finishTimeElement.value) - now;
        if (timeLeft < 0)
            return 0; // Prevents progress bar overjumping
        else
            return timeLeft * 100 / totalTime;
    } else return 100;
}

function setProgress(percent) {
    percent = parseInt(percent);
    const offset = perimeter - percent / 100 * perimeter;
    rect.style.strokeDashoffset = offset;
}

setProgress(getPercentLeft());

// Starting the counter
if (startTimeElement && finishTimeElement) {
    var x = setInterval(() => {
        setProgress(getPercentLeft());
    }, 100);
} else {
    // Game over
    clearInterval(x);
    setProgress(0);
}