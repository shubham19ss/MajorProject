
        $(document).ready(function () {

            $("#customerdetailsform1").hide()
            $("#customerdetailsform2").hide()
            $("#Custidformedit").hide()
            $("#Custidformdelete").hide()
            $("#showcustomerdetails").hide()

            $("#addcustomer").click(function () {
                $("#Custidformedit").hide()
                $("#Custidformdelete").hide()
                $("#customerdetailsform2").hide()
                $("#Feedback").hide()
                $("#customerdetailsform1").slideToggle("slow")
               
            });
           
            $("#AddCustomerButton").click(function(e){
                e.preventDefault();
                var name=$("#addfirstname").val()
                var dob = $("#adddob").val()
                var address = $("#addaddress").val()
                var state = $("#addstate").val()
                var city = $("#addcity").val()
                var pincode = $("#addpincode").val()
                var phonenumber = $("#addphonenumber").val()
                var email = $("#addemail").val()
                if(name.length>0 &&dob.length>0 && address.length>0 && state.length>0 && city.length>0 && pincode.length>0 && phonenumber.length>0 && email.length>0)
                {
                      $.ajax({
                        type: 'POST',
                        url: "http://localhost:53532/ManagerStaff/AddCustomerDetails",
                        async: true,
                        data: { 'Name': name,'DOB':dob,'Address':address,'State':state,'City':city,'Pincode':pincode,'PhoneNumber':phonenumber,'Email':email },
                        success: function (response) {
                            if (response == "success") {
                                $("#customerdetailsform1").hide()
                                $("#Feedback").html("Successfully inserted Data into datebase")
$("#Feedback").show()
                            }

                            else
                                $("#Feedback").html(response)
                        },

                        error: function (result) {
                            //handle any errors      
                            alert(result)
                        }
                    });

                }
                else {
                    $("#Feedback").html("Fill out all Fields")
                }
            });

            $("#editcustomer").click(function () {
                $("#Custidformdelete").hide()
                $("#Feedback").hide()
                $("#customerdetailsform1").hide()
                $("#Custidformedit").slideToggle("slow")
                $("#editmessage").hide()
            });
            $("#editcustomeridsubmit").click(function (e) {
                e.preventDefault();
                var id = $("#customereditid").val();
                if (id.length > 0) {
                  
                    $.ajax({
                        type: 'POST',
                        url: "http://localhost:53532/ManagerStaff/EditCustomer",
                        async: true,
                        data: { 'Custid': id },
                        success: function (result) {
                           
                            if (result != "failure") {
                                var response = eval("(" + result + ")");
                                
                                $("#Efirstname").val(response.CName);
                                mydate = new Date(response.CDOB).toISOString().substr(0, 10);
                                document.querySelector("#Edob").value = mydate;
                                $("#Eaddress").val(response.CAddress);
                                $("#Ecity").val(response.CCity);
                                $("#Estate").val(response.CState);
                                $("#Epincode").val(response.CPin);
                                $("#Ephonenumber").val(response.CTelephone);
                                $("#Eemail").val(response.CEmail);
                                $("#customerdetailsform2").show()
                                $("#Custidformedit").hide();
                            }
                            else {
                                $("#editmessage").html("!!Customer Doesn't Exist!!")
                                $("#editmessage").show()
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
                    
                    $("#editmessage").html("!!!Enter customer id!!!")
                    $("#editmessage").show()
                }
            });
            $("#editcustomersubmit").click(function (e) {
                e.preventDefault();
                var name=$("#Efirstname").val()
                var dob = $("#Edob").val()
                var address = $("#Eaddress").val()
                var state = $("#Estate").val()
                var city = $("#Ecity").val()
                var pincode = $("#Epincode").val()
                var phonenumber = $("#Ephonenumber").val()
                var email = $("#Eemail").val()
              
                if (name.length > 0 && dob.length > 0 && address.length > 0 && state.length > 0 && city.length > 0 && pincode.length > 0 && phonenumber.length > 0 && email.length > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "http://localhost:53532/ManagerStaff/EditCustomerDetails",
                        async: true,
                        data: { 'Name': name, 'DOB': dob, 'Address': address, 'State': state, 'City': city, 'Pincode': pincode, 'PhoneNumber': phonenumber, 'Email': email },
                        success: function (response) {
                            if (response == "success") {
                                $("#customerdetailsform2").hide()
                                $("#Feedback").html("Successfully updated Data into datebase")
                                $("#Feedback").show()
                            }

                            else
                                $("#Feedback").html(response)
                        },

                        error: function (result) {
                            //handle any errors      
                            alert(result)
                        }

                    });
                }
                else {

                    alert("hello")
                    $("#Feedback").html("!!Fill out all Fields!!")
                    $("#Feedback").show()
                }
            });

            $("#deletecustomer").click(function () {
                $("#Custidformdelete").slideToggle("slow")
                $("#Custidformedit").hide()
                $("#customerdetailsform1").hide()
                $("#customerdetailsform2").hide()
                $("#Feedback").hide()

            });
            
            $("#deletecustomeridsubmit").click(function (e){
               
                var id = $("#customerdeleteid").val();
                if (id.length > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "http://localhost:53532/ManagerStaff/EditCustomer",
                        async: true,
                        data: { 'Custid': id },
                        success: function (result) {
                            if (result != null) {
                                var response = eval("(" + result + ")");
                               var result=confirm("Are you Sure you want to delete this record?"+"\n Name:" + response.CName + "\n DOB :" + response.CDOB + "\n Address :" + response.CAddress + "\n City :" + response.CCity + "\n State :" + response.CState + "\n Pincode :" + response.CPin + "\n PhoneNumber :" + response.CTelephone + "\n Email :"+response.CEmail);
                               if(result==true)
                               {

                                   $.ajax({
                                        type: 'POST',
                                       url: "http://localhost:53532/ManagerStaff/DeleteCustomerDetails",
                                       async: true,
                                       data: { 'Custid': id },
                                       success: function (response) {
                                           if (response == "success")
                                               $("#deletemessage").html("Record Deleted Successfully!!")
                                           else
                                             $("#deletemessage").html(response)
                                       }

                                   });

                               }
                               else
                               {
                                   $("#deletemessage").html("Delete Aborted!!")
                               }

                            }
                           
                        },

                        error: function (result) {
                            //handle any errors      
                            alert(result)
                        }
                    });
                }
                else
                    $("#deletemessage").html("!!!Enter customer id!!!")
            });
            $("#showcustomer").click(function (e) {
                e.preventDefault();
                alert("hello")
                $.ajax({
                    type: 'GET',
                    url: "http://localhost:53532/ManagerStaff/ShowDetails",
                    async: true,
                    success: function (response) {
                        if (response == "failure") {
                            $("#Feedback").html("!!No Customers Registered!!")
                            $("#Feedback").show()
                        }
                        else {
                           
                            var result = eval("(" + response + ")")
                            alert("hello")
                            var i, temp;

                            $("#showtable").find("tr:gt(0)").remove();
                            for (i = 0; i < result.length; i++) {
                                var data = "<tr><td>" + result[i].Cid + "</td><td>" + result[i].Custname + "</td><td>" + result[i].CDOB + "</td><td>" + result[i].CustTelephone + "</td><td>" +result[i].Custemail+"</td></tr>";

                                $("#showtable").append(data);
                            }
                            
                            $("#showcustomerdetails").show()
                        }

                    },

                    error: function (result) {
                        //handle any errors      
                        alert(result)
                    }
                });
            });
        });
            
