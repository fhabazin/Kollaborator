MVC provides a way to convert a view model to a JavaScript object. Unfortunately, any client-side changes performed by a user are not reflected in that object. In JavaScript-heavy environments developers will often perform ajax calls to retrieve appropriate markup at any given time. Tha data passed in the requests needs to be current. This is a simple class that will refresh our model-like object with any DOM changes. Now, instead of manually re-reading values, or tracking data changes, we have one reliable source for the data that matters to us most.

jQuery is utilized within this class.


-- Standard Usage --

Reference the class in the view:
<script type="text/javascript" src="~/Scripts/jsmodel.min.js"></script>

Then in script you must instantiate the object:
var obj = new JSModel(@Html.Raw(Json.Encode(Model)));

To update the model at any time simply invoke the event.
var model = obj.refresh();


Loop through all your object properties and output to the console:
for (var propLoop in model) {
   console.log(model[propLoop]);
}

You can also reference an individual property.  This will fetch the value from the model "as is" without doing a refresh first:
var val = obj.prop("FirstName");


--Advanced Usage --

In .NET a user can create properties in their model that are readonly and reference other properties. It's impossible to determine these properties without a server hit.  A URL option exists for that:

var obj = new JSModel([@Html.Raw(Json.Encode(Model))][0], "/Home/GetModel");  //array initialization around @Html.Raw simply done to overcome intellisense shortcoming

Then in the controller simply add some brief code to handle the request:

[HttpPost]
public ActionResult GetModel(MyModel model)
{
   return Json(model);
}

Now you have a complete model to use however you wish.