$(document).ready(function () {
    $("#addform").hide()
    $("#deleteform").hide()
    $("#editform").hide()
    $("#editaccountform").hide()
    $("#deleteaccountform").hide()
    $("#addnewaccount").hide()
    $("#editchangeaccountform").hide()
   
    $("#addaccountbutton").click(function () {
        $("#deleteform").hide()
        $("#editform").hide()
        $("#addnewaccount").slideToggle("slow")
        $("#deleteaccountform").hide()
        $("#editaccountform").hide()
    });
    $("#newcustomeradd").click(function (e) {
        e.preventDefault();
        var custid = $("#newcustomerid").val()
        var accounttype = $("#newcostumeraccounttype").val()
        var dateopened = $("#newcustomeropeningdate").val()
        var statuslist = $("#statuslist").val()
        alert(custid+accounttype+dateopened+statuslist)
        if(custid.length>0 && accounttype.length>0 && dateopened.length>0 && statuslist.length>0)
        {         
                    $.ajax({
                        type: 'POST',
                        url: "http://localhost:53532/ManagerStaff/AddCustomerCheck",
                        async: true,
                        data: { 'Custid': custid },
                        success: function (response) {
                            if (response == "success") {
                                $("#Feedback").html("!!No Customer Exists!!")
                                $("#Feedback").show()
                            }
                            else if (response == "Unexpected error/Server Down") {
                                $("#Feedback").html(response)
                                $("#Feedback").show()
                            }
                            else {
                                $.ajax({
                                    type: 'POST',
                                    url: "http://localhost:53532/Accounts/AddAccountDetails",
                                    async: true,
                                    data: { 'Custid': custid, 'Accounttype': accounttype, 'DateOPened': dateopened, 'Status': statuslist },
                                    success: function (response) {
                                        if (response == "success") {
                                            $("#Feedback").html("Account details Submitted Successfully")
                                            $("#Feedback").show()
                                        }
                                        else {

                                            $("#Feedback").html(response)
                                            $("#Feedback").show()
                                        }

                                    },

                                    error: function (result) {
                                        //handle any errors      
                                        alert(result)
                                    }
                                });

                            }
                               
                        },

                        error: function (result) {
                            //handle any errors      
                            alert(result)
                        }
                    });
                }
               
        else
        {   
            $("#Feedback").html("!!Fill out All Fields!!")
            $("#Feedback").show()
        }

    });
    $("#editaccountbutton").click(function () {
        $("#addnewaccount").hide()
        $("#addform").hide()
        $("#deleteform").hide()
        $("#Feedback").hide()
        $("#editform").slideToggle("slow")
        $("#editaccountform").hide()
        $("#deleteaccountform").hide()
    
    });
    $("#editaccountidsubmit").click(function (e) {
        e.preventDefault();
        var custid = $("#editCid").val()
        if(custid.length>0)
        {$("#editmessage").html("")
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Accounts/AccountCheck",
                async: true,
                data: { 'Custid': custid },
                success: function (response) {
                    if (response == "failure") {
                        $("#Feedback").html("!!No Accounts Registered!!")
                        $("#Feedback").show()
                        $("#editaccountform").hide()
                    }
                    else if (response == "Unexpected error/Server Down") {
                        $("#Feedback").html(response)
                        $("#Feedback").show()
                        $("#editaccountform").hide()
                    }
                    else
                    {   $("#Feedback").hide()

                    $.ajax({
                        type: 'POST',
                        url: "http://localhost:53532/Accounts/EditAccount",
                        async: true,
                        data: { 'Custid': custid },
                        success: function (response) {
                            
                            if (response != "Not found") {
                                var result = eval("(" + response + ")")

                                var i, temp;

                                $("#edittable").find("tr:gt(0)").remove();
                                for (i = 0; i < result.length; i++) {
                                    var data = "<tr><td> <input type='submit'  id=" + result[i].Agid + " class='editclass bg-primary text-center text-light' align='center' style='width:120px;border-radius:5px;margin-top:10px;;padding:10px;' value='Edit' /></td><td>" + result[i].Agid + "</td><td>" + result[i].Agtype + "</td><td>" + result[i].Agdate + "</td><td>" + result[i].Agstatus + "</td><td>" + result[i].Agamount + "</td></tr>";

                                    $("#edittable").append(data);
                                    
                                }

                                $("#editaccountform").show()
                                $(".editclass").click(function (event) {
                                    event.preventDefault()
                                    var inputID = $(this).attr('id');
                                  alert(inputID)
                                    $.ajax({
                                        type: 'POST',
                                        url: "http://localhost:53532/Accounts/GetAccountData",
                                        async: true,
                                        data: { 'Accountid': inputID },
                                        success: function (output) {

                                            var response = eval("(" + output + ")")

                                          
                                            $("#costumeraccounttype").val(response.Agtype)
                                            mydate = new Date(response.Agdate).toISOString().substr(0, 10);
                                            closedate = new Date(response.Aclosedate).toISOString().substr(0, 10);
                                            document.querySelector("#customeropeningdate").value = mydate;
                                            document.querySelector("#customerclosingdate").value = closedate;
                                            $("#editstatuslist").val(response.Agstatus)
                                            $("#editchangeaccountform").show()

                                       },

                                        error: function (result) {
                                            //handle any errors      
                                            alert(result)
                                        }
                                    });

                                    
                                });
                               
                            }
                            else {

                                $("#Feedback").html(response)
                                $("#Feedback").show()
                            }

                        },

                        error: function (result) {
                            //handle any errors      
                            alert(result)
                        }
                    });

                    }
                               
                },

                error: function (result) {
                    //handle any errors      
                    alert(result)
                }
            });
        }
        
        else
        {
            $("#editmessage").html("!!Enter Customer ID!!")
            $("#editmessage").show()
        }

    });
    $("#editcustomeradd").click(function (e) {
        e.preventDefault();

        var type = $("#costumeraccounttype").val()
        var opendate = $("#customeropeningdate").val()
        var closedate = $("#customerclosingdate").val()
        var status = $("#editstatuslist").val()

        if (type.length > 0 && opendate.length > 0 && closedate.length > 0 && status.length > 0)
        {
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Accounts/EditAccountDetails",
                async: true,
                data: { 'Actype': type,'Acopeningdate':opendate,'Acclosingdate':closedate,'Acstatus':status },
                success: function (response) {
                    if (response == "success") {
                        $("#Feedback").html("Account details Submitted Successfully")
                        $("#Feedback").show()
                    }
                    else {

                        $("#Feedback").html(response)
                        $("#Feedback").show()
                    }

                },
                error: function (result) {
                       
                    alert(result)
                }
            });
        }
        else {
            $("#Feedback").html("!!Fill Out all Fields!!")
            $("#Feedback").show()
        }




    });
    $("#deleteaccountbutton").click(function () {
        $("#deleteform").slideToggle("slow")
        $("#addform").hide()
        $("#editform").hide()
        $("#addnewaccount").hide()
    });
    $("#deleteaccountidsubmit").click(function (e) {
        e.preventDefault();
        var custid = $("#deleteCid").val()
        if (custid.length > 0) {
            $("#deletemessage").html("")
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Accounts/AccountCheck",
                async: true,
                data: { 'Custid': custid },
                success: function (response) {
                    if (response == "failure") {
                        $("#Feedback").html("!!No Accounts Registered!!")
                        $("#Feedback").show()
                        $("#editaccountform").hide()
                    }
                    else if (response == "Unexpected error/Server Down") {
                        $("#Feedback").html(response)
                        $("#Feedback").show()
                        $("#editaccountform").hide()
                    }
                    else {
                        $("#Feedback").hide()

                        $.ajax({
                            type: 'POST',
                            url: "http://localhost:53532/Accounts/EditAccount",
                            async: true,
                            data: { 'Custid': custid },
                            success: function (response) {

                                if (response != "Not found") {
                                    var result = eval("(" + response + ")")

                                    var i, temp;

                                    $("#edittable").find("tr:gt(0)").remove();
                                    for (i = 0; i < result.length; i++) {
                                        var data = "<tr><td> <input type='submit'  id=" + result[i].Agid + " class='deleteclass bg-danger text-center text-light' align='center' style='width:120px;border-radius:5px;margin-top:10px;;padding:10px;' value='Delete' /></td><td>" + result[i].Agid + "</td><td>" + result[i].Agtype + "</td><td>" + result[i].Agdate + "</td><td>" + result[i].Agstatus + "</td><td>" + result[i].Agamount + "</td></tr>";

                                        $("#edittable").append(data);

                                    }

                                    $("#editaccountform").show()
                                    $(".deleteclass").click(function (event) {
                                        event.preventDefault()
                                        var inputID = $(this).attr('id');
                                        alert(inputID)
                                        $.ajax({
                                            type: 'POST',
                                            url: "http://localhost:53532/Accounts/GetAccountData",
                                            async: true,
                                            data: { 'Accountid': inputID },
                                            success: function (output) {
    
                                                var response = eval("(" + output + ")")
                                                var result = confirm("Are you Sure you want to delete this record?" + "\n Account No.:" + response.Agid + "\n AccountType :" + response.Agtype + "\n Account Opendate :" + response.Agdate + "\n Account Closedate :" + response.Aclosedate + "\n Account Status :" + response.Agstatus + "\n Amount :" + response.Agamount);
                                                if (result == true) {
                                                    
                                                    $.ajax({
                                                        type: 'POST',
                                                        url: "http://localhost:53532/Accounts/DeleteAccountDetails",
                                                        async: true,
                                                        data: { 'Acid': inputID },
                                                        success: function (response) {
                                                            if (response == "success")
                                                                $("#Feedback").html("Record Deleted Successfully!!")
                                                            else
                                                                $("#Feedback").html(response)
                                                        },
                                                        error: function (result) {
                                                            //handle any errors      
                                                            alert("hello")
                                                        }


                                                    });

                                                }
                                                else {
                                                    $("#Feedback").html("Delete Aborted!!")
                                                }


                                                $("#Feedback").show();

                                            },

                                            error: function (result) {
                                                //handle any errors      
                                                alert(result)
                                            }
                                        });


                                    });

                                }
                                else {

                                    $("#Feedback").html(response)
                                    $("#Feedback").show()
                                }

                            },

                            error: function (result) {
                                //handle any errors      
                                alert(result)
                            }
                        });

                    }

                },

                error: function (result) {
                    //handle any errors      
                    alert(result)
                }
            });
        }

        else {
            $("#deletemessage").html("!!Enter Customer ID!!")
            $("#deletemessage").show()
        }



    });
});

