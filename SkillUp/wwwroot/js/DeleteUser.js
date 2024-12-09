
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