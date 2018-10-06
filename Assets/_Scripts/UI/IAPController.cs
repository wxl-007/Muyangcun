using UnityEngine;
using System.Collections;

public class IAPController : MonoBehaviour {


	const string IAP_PROD_Coins250 = "Coins250";
	const string IAP_PROD_Coins600 = "Coins600";
	const string IAP_PROD_Coins1200 = "Coins1200";
	const string IAP_PROD_Coins3000 = "Coins3000";

	
	const string PANEL_CONNECT_NAME = "Panel2.1.1.0.1_BuyConnecting";
	const string PANEL_BUYCOINS_NAME = "Panel2.1.1.0_BuyCoins";
	
	const string PANEL_SUCCEED_NAME = "Panel2.1.1.0.2_BuySucceed";
	const string PANEL_FAILED_NAME = "Panel2.1.1.0.3_BuyFailed";
	
	public UIPanelManager panelManager;
	public SpriteText totalCoins;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void DoBuyCoins250()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CheckFee250");
        Debug.Log("CheckFee250   succeed");
        //ShowConnectingiTunesPanel();
        //StoreKitBinding.purchaseProduct(IAP_PROD_Coins250, 1);
    }

    public void DoBuyCoins600()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CheckFee600");
        //    ShowConnectingiTunesPanel();
        //    StoreKitBinding.purchaseProduct(IAP_PROD_Coins600, 1);
    }

    public void DoBuyCoins1200()
    {

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CheckFee1200");
        //ShowConnectingiTunesPanel();
        //StoreKitBinding.purchaseProduct(IAP_PROD_Coins1200, 1);
    }

    public void DoBuyCoins3000()
    {

        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CheckFee3000");
        //ShowConnectingiTunesPanel();
        //StoreKitBinding.purchaseProduct(IAP_PROD_Coins3000, 1);
    }



    public void PurchaseSucceed(string productPrice)
    {
        switch (productPrice)
        {
            case IAP_PROD_Coins250:
                // Increase 250 Coins for User
                GlobalManager.totalCoins += 250;
                break;
            case IAP_PROD_Coins600:
                // Increase 600 Coins for User
                GlobalManager.totalCoins += 600;
                break;
            case IAP_PROD_Coins1200:
                // Increase 1200 Coins for User
                GlobalManager.totalCoins += 1200;
                break;
            case IAP_PROD_Coins3000:
                // Increase 3000 Coins for User
                GlobalManager.totalCoins += 3000;
                break;
        }

        GlobalManager.SaveAllToPlayerPrefs();
        // Display the new coins number
        totalCoins.Text = GlobalManager.totalCoins.ToString();

        panelManager.BringIn(PANEL_SUCCEED_NAME);
    }

    public void PurchaseFailed()
    {
        // Back to the buy coins panel
        panelManager.BringIn(PANEL_FAILED_NAME);
    }

    public void PurchaseCancled()
    {
        // Back to the buy coins panel
        panelManager.BringIn(PANEL_BUYCOINS_NAME);
    }


    public void BillingUnlockLevels()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("UnlockBigLevels");
        // Debug.Log("SMS!!!! please wait~~~~");
    }

    public void FeedBackUnlockLevels()
    {
        Debug.Log("purchase succeed !!");
        GlobalManager.BiglevelLock = true;
        GlobalManager.SaveAllToPlayerPrefs();
    }

   
	
	// Do Buy Action
//#if UNITY_IPHONE
//    public void DoBuyCoins250(){
//        ShowConnectingiTunesPanel();
//        StoreKitBinding.purchaseProduct( IAP_PROD_Coins250, 1 );
//    }
	
//    public void DoBuyCoins600(){
//        ShowConnectingiTunesPanel();
//        StoreKitBinding.purchaseProduct( IAP_PROD_Coins600, 1 );
//    }	

//    public void DoBuyCoins1200(){
//        ShowConnectingiTunesPanel();
//        StoreKitBinding.purchaseProduct( IAP_PROD_Coins1200, 1 );
//    }
	
//    public void DoBuyCoins3000(){
//        ShowConnectingiTunesPanel();
//        StoreKitBinding.purchaseProduct( IAP_PROD_Coins3000, 1 );
//    }
//#endif
	
	/* Result
	public void PurchaseSucceed(string productIdentifier){
		// quantity always = 1
		switch (productIdentifier){
			case IAP_PROD_Coins250: 
					// Increase 250 Coins for User
					GlobalManager.totalCoins += 250;
					break;
			case IAP_PROD_Coins600: 
					// Increase 600 Coins for User
					GlobalManager.totalCoins += 600;
					break;
			case IAP_PROD_Coins1200: 
					// Increase 1200 Coins for User
					GlobalManager.totalCoins += 1200;
					break;
			case IAP_PROD_Coins3000: 
					// Increase 3000 Coins for User
					GlobalManager.totalCoins += 3000;
					break;
		}
		GlobalManager.SaveAllToPlayerPrefs();
		// Display the new coins number
		totalCoins.Text = GlobalManager.totalCoins.ToString();
		
		// Back to the buy coins panel
		panelManager.BringIn(PANEL_SUCCEED_NAME);
		
	}
	
	public void PurchaseFailed(){
		// Back to the buy coins panel
		panelManager.BringIn(PANEL_FAILED_NAME);
	}
	
	public void PurchaseCancled(){
		// Back to the buy coins panel
		panelManager.BringIn(PANEL_BUYCOINS_NAME);
	}
	
	
	public void ShowBuyCoinsPanel(){
		panelManager.BringIn(PANEL_BUYCOINS_NAME);
	}
	
	private void ShowConnectingiTunesPanel(){
		if (Application.platform == RuntimePlatform.IPhonePlayer){
			panelManager.BringIn(PANEL_CONNECT_NAME);
		}
	}
	*/


}
