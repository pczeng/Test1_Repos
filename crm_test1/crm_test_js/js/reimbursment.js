
///窗体OnLoad事件
function form_OnLoad() {

}


///窗体OnSave事件
function form_OnSave(executeObj) {
    // 报销金额
    var amount = Xrm.Page.getAttribute("new_total_amount").getValue();
    if (amount > 200) {
        Xrm.Utility.alertDialog("报销金额 > 200");

        // 阻止保存事件
        executeObj.getEventArgs().preventDefault();
    }

}

///报销金额字段OnChange事件
function new_total_amount_OnChange() {

    // 报销金额
    var amount = Xrm.Page.getAttribute("new_total_amount").getValue();

    // new_reimbursementid
    var reimbursement_id = Xrm.Page.data.entity.getId();

    // 备注
    Xrm.Page.getAttribute("new_remark").setValue("报销金额为：" + amount + " id:" + reimbursement_id);

    var obj = getEntityInfo(reimbursement_id);
    console.log(obj);
}

///自定义按钮OnClick事件
function btn1_Action() {

    // 报销金额
    var amount = Xrm.Page.getAttribute("new_total_amount").getValue();

    // new_reimbursementid
    var reimbursement_id = Xrm.Page.getAttribute("new_reimbursementid").getValue();

    Xrm.Utility.alertDialog("自定义按钮 Click 事件, 报销金额:" + amount + " id:" + reimbursement_id);

}

function getEntityInfo(id) {
    id = id.replace("{", "").replace("}", "");
    var webUrl = Xrm.Page.context.getClientUrl() + "/api/data/v9.0/";

    var req = new XMLHttpRequest();
    req.open("GET", encodeURI(webUrl + "new_reimbursements(" + id + ")?$select=new_name, new_total_amount"), false);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json; CHARSET=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");
    req.setRequestHeader("Prefer", "odata.include-annotations=\"OData.Community.Display.V1.FormattedValue\"");
    req.send();
    //console.log(req.status);
    if (req.status == 200) {
        return JSON.parse(req.responseText);
    } else {
        //console.log(req.responseText);
        throw new Error(JSON.parse(req.responseText).error.message);
    }
}


// web api get
function web_api_test_get() {

    var clientURL = Xrm.Page.context.getClientUrl();

    var req = new XMLHttpRequest()
    req.open("GET", encodeURI(clientURL + "/api/data/v9.0/new_reimbursements"), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json; CHARSET=utf-8");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");
    req.onreadystatechange = function () {
        if (this.readyState == 4 /* complete */) {
            req.onreadystatechange = null;
            if (this.status == 204) {
                var resp = this.response;
                console.log("get response: " + resp)
            }
            else {
                var error = JSON.parse(this.response).error;
                console.log(error.message);
            }
        }
    };
    req.send();
}


// web api post
function web_api_test_post() {

    var clientURL = Xrm.Page.context.getClientUrl();

    var req = new XMLHttpRequest()
    req.open("POST", encodeURI(clientURL + "/api/data/v9.0/new_reimbursments"), true);
    req.setRequestHeader("Accept", "application/json");
    req.setRequestHeader("Content-Type", "application/json; CHARSET=gb2312");
    req.setRequestHeader("OData-MaxVersion", "4.0");
    req.setRequestHeader("OData-Version", "4.0");
    req.onreadystatechange = function () {
        if (this.readyState == 4 /* complete */) {
            req.onreadystatechange = null;
            if (this.status == 204) {
                var accountUri = this.getResponseHeader("OData-EntityId");
                console.log("Created account with URI: " + accountUri)
            }
            else {
                var error = JSON.parse(this.response).error;
                console.log(error.message);
            }
        }
    };
    req.send(JSON.stringify({ name: "Sample account" }));

}

