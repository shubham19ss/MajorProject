$(document).ready(function () {
    $("#depositform").hide()
    $("#withdrawform").hide()
    $("#transferform").hide()
    $("#balanceform").hide()
    $("#statementform").hide()
    $("#customisedform").hide()
    $("#Feedback").hide()
    $("#showstatement").hide()
    $("#depositbutton").click(function () {
        $("#depositform").slideToggle("slow")
        $("#withdrawform").hide()
        $("#transferform").hide()
        $("#balanceform").hide()
        $("#statementform").hide()
        $("#customisedform").hide()
        $("#Feedback").hide()
        $("#showstatement").hide()
    });
    $("#depositsubmit").click(function (e) {
        e.preventDefault();
        var acc = $("#accountno").val()
        var amount = $("#amount").val()
        var date = $("#depositdate").val()
        var comment = $("#comment").val()
        alert(acc.length+amount.length+date.length+comment.length)
        if(acc.length>0 && amount.length>0 && date.length>0 && comment.length>0)
        {
           
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Transactions/Deposit",
                async: true,
                data: { 'AccountNo': acc, 'Amount': amount, 'Date': date, 'Comment':comment },
                success: function (response) {
                    if (response == "success") {
                        $("#Feedback").html("Amount Depostied Successfully")
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
        else
        {
            
            $("#depositmessage").html("!!Fill Out all Fields!!")
            $("#depositmessage").show()
        }


    });

    $("#withdrawbutton").click(function () {
        $("#depositform").hide()
        $("#withdrawform").slideToggle("slow")
        $("#transferform").hide()
        $("#balanceform").hide()
        $("#statementform").hide()
        $("#customisedform").hide()
        $("#Feedback").hide()
        $("#showstatement").hide()
    });
    $("#withdrawsubmit").click(function (e) {
        e.preventDefault();
        var acc = $("#waccountno").val()
        var amount = $("#wamount").val()
        var date = $("#withdrawdate").val()
        var comment = $("#wcomment").val()
      
        if (acc.length > 0 && amount.length > 0 && date.length > 0 && comment.length > 0) {

            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Transactions/Withdraw",
                async: true,
                data: { 'AccountNo': acc, 'Amount': amount, 'Date': date, 'Comment': comment },
                success: function (response) {
                    if (response == "success") {
                        $("#Feedback").html("Amount withdrawed Successfully")
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
        else {

            $("#withdrawmessage").html("!!Fill Out all Fields!!")
            $("#withdrawmessage").show()
        }


    });

    $("#transferbutton").click(function () {
        $("#depositform").hide()
        $("#withdrawform").hide()
        $("#transferform").slideToggle("slow")
        $("#balanceform").hide()
        $("#statementform").hide()
        $("#customisedform").hide()
        $("#Feedback").hide()
        $("#showstatement").hide()
       
    });
    $("#transfersubmit").click(function (e) {
        e.preventDefault();

        var sacc = $("#tsaccountno").val()
        var dacc = $("#tdaccountno").val()
        var amount = $("#tamount").val()
        var date = $("#tdrawdate").val()
        var comment = $("#tcomment").val()

        if (dacc.length > 0 && sacc.length > 0 && amount.length > 0 && date.length > 0 && comment.length > 0) {

            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Transactions/Transfer",
                async: true,
                data: { 'SAccountNo': sacc, 'DAccountNo': dacc, 'Amount': amount, 'Date': date, 'Comment': comment },
                success: function (response) {
                    if (response == "success") {
                        $("#Feedback").html("Amount tranferred Successfully")
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
        else {

            $("#transfermessage").html("!!Fill Out all Fields!!")
            $("#transfermessage").show()
        }


    });
    $("#balancebutton").click(function () {
        $("#depositform").hide()
        $("#withdrawform").hide()
        $("#transferform").hide()
        $("#balanceform").slideToggle("slow")
        $("#statementform").hide()
        $("#customisedform").hide()
        $("#Feedback").hide()
        $("#showstatement").hide()
    });
    $("#balancesubmit").click(function (e) {
        e.preventDefault();
        var accountno = $("#baccountno").val()
        if(accountno.length>0)
        {
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Transactions/Balance",
                async: true,
                data: { 'SAccountNo': accountno },
                success: function (response) {
                    if (response == "!!No Account Exists!!") {
                        $("#Feedback").html(response)
                        $("#Feedback").show()
                    }
                    else {

                        $("#Feedback").html("Balance Amount is  "+response+"/-")
                        $("#Feedback").show()
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
            $("#balancemessage").html("!!Enter Account No!!")
            $("#balancemessage").show()
        }
    });
    $("#statementbutton").click(function () {
        $("#depositform").hide()
        $("#withdrawform").hide()
        $("#transferform").hide()
        $("#balanceform").hide()
        $("#statementform").slideToggle("slow")
        $("#customisedform").hide()
        $("#Feedback").hide()
        $("#showstatement").hide()
    });
    $("#statementsubmit").click(function (e) {
        e.preventDefault();
       
        var accountno = $("#saccountno").val()
        if(accountno.length>0)
        {
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Transactions/Statement",
                async: true,
                data: { 'SAccountNo': accountno },
                success: function (response) {
                    if (response == "failure") {
                        $("#Feedback").html("!!No Account!!")
                        $("#Feedback").show()
                    }
                    else if (response == "!!server Error!!") {
       
                       
                        $("#Feedback").html(response)
                        $("#Feedback").show()
                    }
                       
                    else {
                        
                        var result = eval("(" + response + ")")
                        
                        var i, temp;
                    
                        $("#transactiontable").find("tr:gt(0)").remove();
                        for(i=0;i<result.length;i++)
                        {
                            var data = "<tr><td>" + result[i].Tid + "</td><td>" + result[i].Tamount + "</td><td>" + result[i].Ttype + "</td><td>" + result[i].Tdepositdate + "</td><td>" + result[i].Tcomment + "</td></tr>";
                         
                            $("#transactiontable").append(data);
                        }
                        
                        $("#showstatement").show()
                    }

                },
                error: function (result) {
                    //handle any errors      
                    alert(result)
                }
            });
           
        }
        else {
            $("#statementmessage").html("!!Enter Account No!!")
            $("#statementmessage").show()
        }
    });
    $("#cstatementbutton").click(function () {
        $("#depositform").hide()
        $("#withdrawform").hide()
        $("#transferform").hide()
        $("#balanceform").hide()
        $("#statementform").hide()
        $("#customisedform").slideToggle("slow")
        $("#showstatement").hide()
        $("#Feedback").hide()
    });
    $("#customisedsubmit").click(function (e) {
        e.preventDefault();
        $("#Feedback").hide()
        var accountno = $("#caccountno").val()
        var fromdate = $("#fromdate").val()
        var todate=$("#todate").val()
        if(accountno.length>0 && fromdate.length>0 && todate.length>0)
        { 
            $.ajax({
                type: 'POST',
                url: "http://localhost:53532/Transactions/Customised",
                async: true,
                data: { 'cAccountNo':accountno, 'FromDate': fromdate, 'ToDate': todate},
                success: function (response) {
                    if (response == "failure") {
                        $("#Feedback").html("!!No Account Registered!!")
                        $("#Feedback").show()
                    }
                    else if (response == "!!server Error!!" || response == "!!Please check Dates Entered!!") {


                        $("#Feedback").html(response)
                        $("#Feedback").show()
                    }
                  else  {

                        var result = eval("(" + response + ")")

                        var i, temp;

                        $("#transactiontable").find("tr:gt(0)").remove();
                        for (i = 0; i < result.length; i++) {
                            var data = "<tr><td>" + result[i].Tid + "</td><td>" + result[i].Tamount + "</td><td>" + result[i].Ttype + "</td><td>" + result[i].Tdepositdate + "</td><td>" + result[i].Tcomment + "</td></tr>";

                            $("#transactiontable").append(data);
                        }
                        $("#customisedform").hide()
                        $("#showstatement").show()
                    }

                },

                error: function (result) {
                    //handle any errors      
                    alert(result)
                }
            });

        }

        else {
            $("#customisedmessage").html("!!Fill Out all Fields!!")
            $("#customisedmessage").show()
        }

    });

});
