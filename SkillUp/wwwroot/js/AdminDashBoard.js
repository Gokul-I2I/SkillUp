window.UserManagement = function () {
    const userForm = document.getElementById("userButton");
    if (userForm) {
        userForm.dispatchEvent(new Event("submit")); // Trigger the submit event
    } else {
        console.error("User button not found.");
    }
};

document.addEventListener("DOMContentLoaded", () => {
    const userButton = document.getElementById("userButton");
    if (userButton) {
        userButton.addEventListener("click", () => {
            window.location.href = "/UserDashBoard/Index";
        });
    }
});
