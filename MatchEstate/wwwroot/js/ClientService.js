import { StatusDivSetter } from "./StatusDivSetter";
class ClientService extends StatusDivSetter{
    static clients = [];

    addClient() {

    }

    static deleteClient(clientIds) {
        if (clientIds.length != 0) {
            $.post("/client/DeleteClient", { clientsIds: clientIds }, (data, status) => {
              
                if (data.success) {
                    this.showStatusDiv("success", "Selected clients have been deleted.", 2000);

                    setTimeout(() => {
                        location.reload();
                    }, 2000);
                }
                else {
                    this.showStatusDiv("danger", data.message, 2000);
                }
            })
        }
        else {
            this.showStatusDiv("danger", "Please select the listings to delete.", 2000);
        }
    }

    static getByFilters(search) {
        $("tbody").html('<div class="show d-flex align-items-center justify-content-center w-100"><div class="spinner-border text-primary text-center" style = "width: 3rem; height: 3rem;" role = "status"><span class="sr-only" > Loading...</span></div></div>');
        $.get("/client/searchClient", {
            search: search
        }, (data, status) => {
            if (data.success) {
                this.clients = data.data;

                let count = clients.length;
                let pageCount = count / 20;
                let netPageCount = Math.floor(pageCount);
                if (pageCount - netPageCount > 0) {
                    netPageCount++;
                }

                this.showClients(1);
            }
        }
        ).fail(function (xhr, status) {
            $("tbody").html(`<div class='alert-danger text-center'>An error occured. Error: ${xhr} - ${status}</div>`);
        });
    }

    static showClients(pageNumber) {
        let startIndex = (pageNumber - 1) * 20;
        let finishIndex = pageNumber * 20;
        let html = "";
        if (clients.length == 0) {
            $("tbody").html(`<div class='alert-warning text-center'>Client couldn't be found.</div>`);
        }
        for (i = startIndex; i < finishIndex; i++) {
            if (clients[i] != null || clients[i] != undefined) {
                html += `
                                     <tr>
                                                        <td><input name="clients" class="form-check-input" type="checkbox" value=${clients[i].id}></td>
                                                            <td>${clients[i].nameSurname}</td>
                                                            <td>${clients[i].phoneNumber}</td>
                                                            <td>${clients[i].email}</td>
                                                    </tr>
                        `
                $("tbody").html(html);
            }
            else {
                break;
            }
        };

    }
}