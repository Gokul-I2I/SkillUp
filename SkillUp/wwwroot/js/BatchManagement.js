async function confirmDelete(batchId) {
    if (confirm("Are you sure you want to delete this Batch?")) {
        try {
            const response = await fetch(`https://localhost:7202/api/batch/${batchId}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                alert("Batch deactiveted successfully.");
                location.reload();
            } else {
                alert("Failed to delete bactch. Please try again.");
            }
        } catch (error) {
            console.error("Error during deletion:", error);
            alert("An error occurred. Please try again.");
        }
    }
}
async function confirmActive(batchId) {
    try {
        const response = await fetch(`https://localhost:7202/api/batch/${batchId}`, {
            method: 'PATCH'
        });

        if (response.ok) {
            alert("Batch activated successfully.");
            location.reload();
        } else {
            alert("Failed to activate batch. Please try again.");
        }
    } catch (error) {
        console.error("Error during activation:", error);
        alert("An error occurred. Please try again.");
    }
}