export class StatusDivSetter {
    showStatusDiv(status, message, displayTime) {
        $("#statusDiv").html(`
            <div class="alert-${status}">
                ${message}
            </div>
        `);
        $("#statusDiv").fadeIn(2000, () => {
            setTimeout(() => {
                $("#statusDiv").fadeOut();
            }, displayTime);
        })
    }
}