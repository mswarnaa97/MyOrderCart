Blazor Shopping Cart with REST API Product List

Here’s how it works. 
Select an item from the store (in this case a list). The details of that item are shown below the list. Enter a quantity and add it to your cart. The contents of your cart are shown below that, including the grand total.

Reference the System.Net.Http.Json NuGet package in the project file.

 services.AddScoped<HttpClient>(s =>
            {
                return new HttpClient { BaseAddress = new Uri(@"https://fakestoreapi.com/") };
            });

Every time you update the cart, the contents are serialized and stored in the browser’s local storage using a not-as-yet-released package, 

Microsoft.AspNetCore.ProtectedBrowserStorage. 
 
  public DateTime LastAccessed { get; set; }
    public int TimeToLiveInSeconds { get; set; } = 30; // default

Whenever the Cart is persisted to localStorage, the LastAccessed time stamp is saved. When loading the cart from localStorage, a check is made to see if the cart has expired based on the LastAccessed and TimeToLiveInSeconds properties. I set it to 30 seconds by default so you could easily test it without having to wait around for a long period of time to elapse.

THE CODE

I created a Blazor Server project in Visual Studio 2022 called MyOrderCart

Let’s start with the three models:

 public class Product

  public class CartProduct

   public class Cart 

   Then we have a CartProduct which represents a Product and Quantity, and has a Total property that multiplies the product price by the quantity.

Finally, the Cart object has a list of CartProducts and the aforementioned properties to support expiration. This is a greatly simplified shopping cart. The focus here should be on the persistence patterns. 

Following Steve’s instruction, I added the Microsoft.AspNetCore.ProtectedBrowserStorage package to the project.

Next, I added this line to my ConfigureServices method in Startup.cs:

services.AddProtectedBrowserStorage();

Next, I created a component called CartStateProvider.razor to handle loading and saving the cart:

@inject ProtectedLocalStorage ProtectedLocalStore

This component has an instance of Cart (ShoppingCart) which it loads and saves to localStorage. 

Finally, we can use it in the Index page (the default page for Blazor projects):

Everything starts at the OnInitialized method. That’s where I create two products and add them to the Products list.

Now look at the top of the page. If the Products list is loaded, we show the products in a select element.

When an item is selected, the ProductSelected method fires, and the SelectedProduct is set.

Next, we have logic that shows the product info if SelectedProduct isn’t null and the boolean ShowItem is set to true. That table includes an input element for the quantity and an Add button that calls the AddToCart method when clicked.

The AddToCart method then creates a CartProduct, adds it to the CartStateProvider.ShoppingCart, and persists it.

Next, if the CartStateProvider.ShoppingCart isn’t null and actually contains items, it is shown below, again in a table element. Each item has a Remove button that when clicked calls RemoveItem, which removes the item from the cart and again saves the cart.

Take a look at the expiry code in the CartStateProvider’s OnParametersSetAsync method. This happens when the page loads and the cart gets instantiated. This is the critical check right here: