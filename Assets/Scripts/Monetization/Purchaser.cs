using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, one of the existing Survival Shooter scripts.

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener{
	private static IStoreController m_StoreController;                                                                  // Reference to the Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;                                                         // Reference to store-specific Purchasing subsystems.

	public MoneyDisplay money;
	// Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts 
	// for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

//	private static string kProductIDConsumable =    "consumable";                                                         // General handle for the consumable product.
	private static string productID_3kBulles = "3kBulles";
	private static string productID_15kBulles = "15kBulles";
	private static string productID_30kBulles = "30kBulles";
	private static string productID_60kBulles = "60kBulles";
	private static string productID_150kBulles = "150kBulles";
	private static string productID_250kBulles = "250kBulles";
	private static string productID_500kBulles = "500kBulles";
	private static string productID_DoubleBulle = "DoubleBulle";

//	private static string kProductNameAppleConsumable =    "com.unity3d.test.services.purchasing.consumable";             // Apple App Store identifier for the consumable product.
	private static string productKeyApple_3kBulles = "";
	private static string productKeyApple_15kBulles = "";
	private static string productKeyApple_30kBulles = "";
	private static string productKeyApple_60kBulles = "";
	private static string productKeyApple_150kBulles = "";
	private static string productKeyApple_250kBulles = "";
	private static string productKeyApple_500kBulles = "";
	private static string productKeyApple_DoubleBulle = "";

//	private static string kProductNameGooglePlayConsumable =    "com.unity3d.test.services.purchasing.consumable";        // Google Play Store identifier for the consumable product.
	private static string productKeyAndroid_3kBulles = "3kbulles";
	private static string productKeyAndroid_15kBulles = "15kbulles";
	private static string productKeyAndroid_30kBulles = "30kbulles";
	private static string productKeyAndroid_60kBulles = "60kbulles";
	private static string productKeyAndroid_150kBulles = "150kbulles";
	private static string productKeyAndroid_250kBulles = "250kbulles";
	private static string productKeyAndroid_500kBulles = "500kbulles";
	private static string productKeyAndroid_DoubleBulle = "2xbulle";


	void Start()
	{
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null)
		{
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
		if (money == null) {
			money = FindObjectOfType<MoneyDisplay> ();
		}
	}

	public void InitializePurchasing() 
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized())
		{
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

		// Add a product to sell / restore by way of its identifier, associating the general identifier with its store-specific identifiers.
		//builder.AddProduct(kProductIDConsumable, ProductType.Consumable, new IDs(){{ kProductNameAppleConsumable,       AppleAppStore.Name },{ kProductNameGooglePlayConsumable,  GooglePlay.Name },});// Continue adding the non-consumable product.
		builder.AddProduct(productID_3kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_3kBulles, AppleAppStore.Name}, {productKeyAndroid_3kBulles, GooglePlay.Name}, });
		builder.AddProduct(productID_15kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_15kBulles, AppleAppStore.Name}, {productKeyAndroid_15kBulles, GooglePlay.Name}, });
		builder.AddProduct(productID_30kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_30kBulles, AppleAppStore.Name}, {productKeyAndroid_30kBulles, GooglePlay.Name}, });
		builder.AddProduct(productID_60kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_60kBulles, AppleAppStore.Name}, {productKeyAndroid_60kBulles, GooglePlay.Name}, });
		builder.AddProduct(productID_150kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_150kBulles, AppleAppStore.Name}, {productKeyAndroid_150kBulles, GooglePlay.Name}, });
		builder.AddProduct(productID_250kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_250kBulles, AppleAppStore.Name}, {productKeyAndroid_250kBulles, GooglePlay.Name}, });
		builder.AddProduct(productID_500kBulles, ProductType.Consumable, new IDs(){ {productKeyApple_500kBulles, AppleAppStore.Name}, {productKeyAndroid_500kBulles, GooglePlay.Name}, });
		builder.AddProduct (productID_DoubleBulle, ProductType.NonConsumable, new IDs () { {productKeyApple_DoubleBulle, AppleAppStore.Name},{productKeyAndroid_DoubleBulle, GooglePlay.Name}, });
		UnityPurchasing.Initialize(this, builder);
	}


	private bool IsInitialized()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	/*public void BuySubscription()
	{
		// Buy the subscription product using its the general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID(kProductIDSubscription);
	}*/

	public void Buy3KBulles(){
		BuyProductID (productID_3kBulles);
	}

	public void Buy15KBulles(){
		BuyProductID (productID_15kBulles);
	}

	public void Buy30KBulles(){
		BuyProductID (productID_30kBulles);
	}

	public void Buy60KBulles(){
		BuyProductID (productID_60kBulles);
	}

	public void Buy150KBulles(){
		BuyProductID (productID_150kBulles);
	}

	public void Buy350KBulles(){
		BuyProductID (productID_250kBulles);
	}

	public void Buy600KBulles(){
		BuyProductID (productID_500kBulles);
	}
	public void BuyDoubleBulle(){
		BuyProductID (productID_DoubleBulle);
	}

	void BuyProductID(string productId)
	{
		// If the stores throw an unexpected exception, use try..catch to protect my logic here.
		try
		{
			// If Purchasing has been initialized ...
			if (IsInitialized())
			{
				// ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
				Product product = m_StoreController.products.WithID(productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				if (product != null && product.availableToPurchase)
				{
					Debug.Log (string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
					Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
					Values.MakeToast("Product couldn't be bought, sorry !");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
				Debug.Log("BuyProductID FAIL. Not initialized.");
				Values.MakeToast("Purshaser not initialized, problem on my side, sorry !");
			}
		}
		// Complete the unexpected exception handling ...
		catch (Exception e)
		{
			// ... by reporting any unexpected exception for later diagnosis.
			Debug.Log ("BuyProductID: FAIL. Exception during purchase. " + e);
			Values.MakeToast ("Exception during purshase, sorry !");
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			Values.MakeToast ("RestorePurchases FAIL. Not initialized. Sorry !");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");
			Values.MakeToast ("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		Values.MakeToast ("OnInitializeFailed, big problem, sorry ! " + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		// A consumable product has been purchased by this user.
		if (String.Equals (args.purchasedProduct.definition.id, productID_3kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (3000);
			Values.GetMoneyCounter ().SaveMoney ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_15kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (15000);
			Values.GetMoneyCounter ().SaveMoney ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_30kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (30000);
			Values.GetMoneyCounter ().SaveMoney ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_60kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (60000);
			Values.GetMoneyCounter ().SaveMoney ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_150kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (150000);
			Values.GetMoneyCounter ().SaveMoney ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_250kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (350000);
			Values.GetMoneyCounter ().SaveMoney ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_500kBulles, StringComparison.Ordinal)) {
			Values.GetMoneyCounter ().AddMoney (600000);
			Values.GetMoneyCounter ().SaveMoney ();
			Values.inventory.color_golden = true;
			Values.SaveInventory ();
		} else if (String.Equals (args.purchasedProduct.definition.id, productID_DoubleBulle, StringComparison.Ordinal)) {
			Values.moneyMultiplier = 2;
			Values.SaveValues ();
		}
		else{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
			Values.MakeToast ("ProcessPurchase: FAIL. Unrecognized product: " + args.purchasedProduct.definition.id);
		}// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.

		if (money == null) {
			money = FindObjectOfType<MoneyDisplay> ();
			money.UpdateText ();
			print ("MONEY DISPLAY NULL");
			//Values.MakeToast("MONET DISPLAY NULL", 10f);
		} else {
			money.UpdateText ();
		}

		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",product.definition.storeSpecificId, failureReason));
		Values.MakeToast ("Purchase failed ! Because : " + failureReason);
	}
}

