class ProgressBar {
    constructor(targetElement) {
        this.targetElement = targetElement;
        this.width = targetElement.clientWidth;
        this.height = targetElement.clientHeight;
        this.perimeter = (this.width + this.height) * 2;

        this.insertFourBorders();
        this.setProgress(0);
    }

    setProgress(percent) {
        if (percent >= 0 && percent <= 100) {
            const totalPxToShow = Math.floor(this.perimeter * parseInt(percent) / 100);
            const distribution = this.calcPixelsToShowForEachBorder(totalPxToShow);
            
            if (this.borderGroup.children.length >= 4) {
                this.borderGroup.children[0].style.right = this.numToPx(this.width - distribution["top"]);
                this.borderGroup.children[1].style.bottom = this.numToPx(this.height - distribution["right"]);
                this.borderGroup.children[2].style.left = this.numToPx(this.width - distribution["bottom"]);
                this.borderGroup.children[3].style.top = this.numToPx(this.height - distribution["left"]);
            } else console.alert("Missing borders problem!");
        }
    }

    calcPixelsToShowForEachBorder(totalPxToShow) {
        const resultObj = { top: 0, right: 0, bottom: 0, left: 0 };
        if (totalPxToShow >= this.width) {
            resultObj["top"] = this.width;
            totalPxToShow -= this.width;
        } else {
            resultObj["top"] = totalPxToShow;
            return resultObj;
        }

        if (totalPxToShow >= this.height) {
            resultObj["right"] = this.height;
            totalPxToShow -= this.height;
        } else {
            resultObj["right"] = totalPxToShow;
            return resultObj;
        }

        if (totalPxToShow >= this.width) {
            resultObj["bottom"] = this.width;
            totalPxToShow -= this.width;
        } else {
            resultObj["bottom"] = totalPxToShow;
            return resultObj;
        }

        if (totalPxToShow >= this.height) {
            resultObj["left"] = this.height;
            totalPxToShow -= this.height;
        } else {
            resultObj["left"] = totalPxToShow;
            return resultObj;
        }
        return resultObj;
    }

    insertFourBorders() {
        var borderGroupDiv = document.createElement("div");
        var ids = ["top", "right", "bottom", "left"];

        for (const id of ids) {
            let newBorderDiv = document.createElement("div");
            newBorderDiv.id = id;
            newBorderDiv.className = "progress-border";

            if (id == "top") {
                newBorderDiv.style.right = this.numToPx(this.width - 100);
            } else if (id == "right") {
                newBorderDiv.style.bottom = this.numToPx(this.height);
            } else if (id == "bottom") {
                newBorderDiv.style.left = this.numToPx(this.width);
            } else if (id == "left") {
                newBorderDiv.style.top = this.numToPx(this.height);
            }
            
            borderGroupDiv.appendChild(newBorderDiv);
        }

        borderGroupDiv.className = "borders";
        if (this.targetElement.firstChild) {
            this.targetElement.insertBefore(borderGroupDiv, this.targetElement.firstChild);
        } else {
            this.targetElement.appendChild(borderGroupDiv);
        }
        this.borderGroup = borderGroupDiv;
    }

    numToPx(number) {
        return number.toString() + "px";
    }
}

class TimeControlledProgressBar {
    refreshInterval = 250;

    constructor(progressBar) {
        this.progressBar = progressBar;
        this.startTimeElement = document.getElementById("start-time-input");
        this.finishTimeElement = document.getElementById("finish-time-input");
        this.progressBar.setProgress(100);
    }

    getPercentLeft() {
        if (this.startTimeElement && this.finishTimeElement) {
            var now = new Date().getTime();
            const totalTime = parseInt(this.finishTimeElement.value) - parseInt(this.startTimeElement.value);
            const timeLeft = parseInt(this.finishTimeElement.value) - now;
            if (timeLeft < 0)
                return 0; // Prevents progress bar overjumping
            else
                return timeLeft * 100 / totalTime;
        } else return 100;
    }

    startTimer() {
        this.timer = setInterval(() => {
            if (this.finishTimeElement) {
                if (new Date().getTime() <= this.finishTimeElement.value) {
                    this.progressBar.setProgress(parseInt(this.getPercentLeft()));
                } else {
                    console.log("Timer cleared!");
                    this.progressBar.setProgress(0);
                    clearInterval(this.timer);
                }
            }
        }, this.refreshInterval);
    }

}

const progressBarContainer = document.querySelector(".wrapped-progress-bar")
var progressBar;

if (progressBarContainer) {
    progressBarObj = new ProgressBar(progressBarContainer);
    //progressBarObj.setProgress(56);
}

const timedProgressBar = new TimeControlledProgressBar(progressBarObj);
timedProgressBar.startTimer();