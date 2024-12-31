document.addEventListener("DOMContentLoaded", () => {
 
    const userManagementCard = document.querySelector(".user-management");
    userManagementCard.addEventListener("click", function () {
        window.location.href = "/UserDashBoard/Index"; 
    });

    const batchManagementCard = document.querySelector(".batch-management");
    batchManagementCard.addEventListener("click", function () {
        window.location.href = "/BatchDashBoard/Index"; 
    });
  
});
