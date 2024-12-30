document.addEventListener("DOMContentLoaded", () => {
    const userButton = document.getElementById("userButton");
    if (userButton) {
        userButton.addEventListener("click", () => {
            window.location.href = "/UserDashBoard/Index"; 
        });
    } else {
        console.error("User button not found.");
    }

    const batchButton = document.getElementById("batchButton");
    if (batchButton) {
        batchButton.addEventListener("click", () => {
            window.location.href = "/Batch/Index"; 
        });
    } else {
        console.error("Batch button not found.");
    }
});
