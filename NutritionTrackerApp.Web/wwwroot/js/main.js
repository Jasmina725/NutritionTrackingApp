const apiBase = "https://localhost:7152/api/foodentry";
let entryToDelete = null; 

function getToken() {
    return localStorage.getItem("jwtToken");
}

$(document).ready(function () {
    if ($("#todayTable").length) loadToday();

    if ($("#entriesTable").length) loadEntries();
    // Logout
    $("#logoutBtn").click(() => {
        localStorage.removeItem("jwtToken");
        window.location.href = "/index.html";
    });

    // Save Entry
    $("#saveEntry").click(() => {
        const name = $("#ingredientName").val().trim();
        const amount = parseFloat($("#quantity").val());
        const unit = $("#unit").val();
        if (!name || isNaN(amount)) return;
        addEntry(0, name, amount, unit);
    });
});

// AJAX Functions
async function loadToday() {
    const token = getToken();
    const tbody = $("#todayTable tbody");
    tbody.empty();

    const today = new Date().toISOString();

    try {
        const data = await $.ajax({
            url: `${apiBase}/day/${today}`,
            method: "GET",
            headers: { "Authorization": `Bearer ${token}` }
        });
        let totalCalories = 0;
        data.forEach(entry => {
            tbody.append(`
                <tr>
                    <td>${entry.ingredientName}</td>
                    <td>${entry.amount}</td>
                    <td>${entry.unit}</td>
                    <td>${entry.calories}</td>
                    <td class="text-end"><button class="btn btn-outline-danger btn-rounded btn-sm" onclick="openDeleteEntryModal('${entry.id}')">Delete</button></td>
                </tr>
            `);
            totalCalories += entry.calories
        });
        $("#todayTotalCalories").text(totalCalories);

    } catch (err) {
        console.error("Failed to load today's entries", err);
    }
}
async function loadEntries() {
    const token = getToken();
    if (!token) { window.location.href = "/index.html"; return; }

    try {
        const data = await $.ajax({
            url: `${apiBase}/days`, method: "GET",
            headers: { "Authorization": `Bearer ${token}` }
        });
        const tbody = $("#entriesBody"); tbody.empty();
        data.forEach(entry => {
            tbody.append(`<tr class="entry-row" data-id="${entry.date}" data-date="${entry.date}">
                <td>${new Date(entry.date).toLocaleDateString()}</td>
                <td>${entry.totalCalories}</td>
                <td class="text-end">
                <button class="btn btn-outline-primary btn-rounded btn-sm" onclick="openDailyDetails('${entry.date}')">
                    Details
                </button>
                <button class="btn btn-outline-danger btn-rounded btn-sm">
                    Delete
                </button>
            </td>
            </tr>`);
        });
    } catch (err) { console.error(err); }
}

async function addEntry(ingredientId, name, amount, unit) {
    try {
        const token = getToken();
        const response = await $.ajax({
            url: `${apiBase}`,
            method: "POST",
            contentType: "application/json",
            headers: {
                "Authorization": `Bearer ${token}`
            },
            data: JSON.stringify({ ingredientId, name, amount, unit })
        });

        const addEntryModalEl = document.getElementById("addEntryModal");
        const modalInstance = bootstrap.Modal.getInstance(addEntryModalEl)
            || new bootstrap.Modal(addEntryModalEl);
        modalInstance.hide();

        $("#ingredientName").val("");
        $("#quantity").val("");
        $("#unit").val("g");

        await loadToday();
    } catch (err) {
        console.error("Error adding entry:", err);
    }
}

async function openDeleteEntryModal(entryId) {
    entryToDelete = entryId;
    $('#confirmDeleteModal').modal('show');
}

async function deleteEntry() {
    try {
        const token = getToken();
        await $.ajax({
            url: `${apiBase}/${entryToDelete}`,
            method: "DELETE",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });
        $('#confirmDeleteModal').modal('hide');
        await loadToday();
    } catch (err) {
        console.error("Error deleting entry:", err);
    }
}

async function openDailyDetails(date) {
    const token = localStorage.getItem("jwtToken");
    if (!token) {
        window.location.href = "/index.html";
        return;
    }

    try {
        const data = await $.ajax({
            url: `${apiBase}/day/${date}`,
            method: "GET",
            headers: { "Authorization": `Bearer ${token}` }
        });
        let dateOnly = new Date(date).toLocaleDateString('en-GB');
        $("#dailyDetailsTitle").text(`Daily Details – ${dateOnly}`);

        const tbody = $("#dailyDetailsBody");
        tbody.empty();

        let totals = {
            calories: 0,
            protein: 0,
            carbs: 0,
            fat: 0,
            sugar: 0
        };

        data.forEach(entry => {
            tbody.append(`
                <tr>
                    <td>${entry.ingredientName}</td>
                    <td>${entry.amount}</td>
                    <td>${entry.unit}</td>
                    <td>${entry.calories}</td>
                    <td>${entry.protein}</td>
                    <td>${entry.carbs}</td>
                    <td>${entry.fat}</td>
                    <td>${entry.sugar}</td>
                </tr>
            `);

            totals.calories += entry.calories;
            totals.protein += entry.protein;
            totals.carbs += entry.carbs;
            totals.fat += entry.fat;
            totals.sugar += entry.sugar;
        });
        $("#totalCalories").text(totals.calories.toFixed(2));
        $("#totalProtein").text(totals.protein.toFixed(2));
        $("#totalCarbs").text(totals.carbs.toFixed(2));
        $("#totalFat").text(totals.fat.toFixed(2));
        $("#totalSugar").text(totals.sugar.toFixed(2));

        $("#sliderCalories").val(Math.min(100, (totals.calories / 2000 * 100)));
        $("#sliderProtein").val(Math.min(100, (totals.protein / 150 * 100)));
        $("#sliderCarbs").val(Math.min(100, (totals.carbs / 300 * 100)));
        $("#sliderFat").val(Math.min(100, (totals.fat / 70 * 100)));
        $("#sliderSugar").val(Math.min(100, (totals.sugar / 50 * 100)));

        $("#dailyDetailsModal").modal("show");

    } catch (err) {
        console.error("Daily details error:", err);
        alert("Could not load daily details.");
    }
}