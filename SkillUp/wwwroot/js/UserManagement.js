window.UserManagement = function () {
    const userButton = document.getElementById("userButton");
    if (userButton) {
        userButton.dispatchEvent(new Event("click")); // Trigger click event
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

async function confirmDelete(userId) {
    if (confirm("Are you sure you want to delete this user?")) {
        try {
            const response = await fetch(`https://localhost:7202/api/user/${userId}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                alert("User deleted successfully.");
                location.reload();
            } else {
                alert("Failed to delete user. Please try again.");
            }
        } catch (error) {
            console.error("Error during deletion:", error);
            alert("An error occurred. Please try again.");
        }
    }
}
async function confirmActive(userId) {
    try {
        const response = await fetch(`https://localhost:7202/api/user/${userId}`, {
            method: 'PATCH'
        });

        if (response.ok) {
            alert("User activated successfully.");
            location.reload();
        } else {
            alert("Failed to activate user. Please try again.");
        }
    } catch (error) {
        console.error("Error during activation:", error);
        alert("An error occurred. Please try again.");
    }
}