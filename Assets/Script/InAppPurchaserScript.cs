using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using UnityEngine.Purchasing;

public class InAppPurchaserScript : MonoBehaviour, IStoreListener {

    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    #region 상품ID
    // 상품ID는 구글 개발자 콘솔에 등록한 상품ID와 동일하게 해주세요.
    public const string productId1 = "gem1";
    public const string productId2 = "gem2";
    public const string productId3 = "gem3";
    public const string productId4 = "gem4";
    public const string productId5 = "gem5";
    public const string productId6 = "gem6";
    #endregion

    public Text GemText;
    void Start()
    {
        InitializePurchasing();
    }

    private bool IsInitialized()
    {
        return (storeController != null && extensionProvider != null);
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(productId1, ProductType.Consumable, new IDs
        {
            { productId1, AppleAppStore.Name },
            { productId1, GooglePlay.Name },
        });

        builder.AddProduct(productId2, ProductType.Consumable, new IDs
        {
            { productId2, AppleAppStore.Name },
            { productId2, GooglePlay.Name }, }
        );

        builder.AddProduct(productId3, ProductType.Consumable, new IDs
        {
            { productId3, AppleAppStore.Name },
            { productId3, GooglePlay.Name },
        });

        builder.AddProduct(productId4, ProductType.Consumable, new IDs
        {
            { productId4, AppleAppStore.Name },
            { productId4, GooglePlay.Name },
        });

        builder.AddProduct(productId5, ProductType.Consumable, new IDs
        {
            { productId5, AppleAppStore.Name },
            { productId5, GooglePlay.Name },
        });
        builder.AddProduct(productId5, ProductType.Consumable, new IDs
        {
            { productId6, AppleAppStore.Name },
            { productId6, GooglePlay.Name },
        });

        UnityPurchasing.Initialize(this, builder);
    }
    public void ClickBuyBtn()                   //
    {
        if (EventSystem.current.currentSelectedGameObject.name.Equals("1100won"))
        {
            BuyProductID(productId1);
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("3300won"))
        {
            BuyProductID(productId2);
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("5500won"))
        {
            BuyProductID(productId3);
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("11000won"))
        {
            BuyProductID(productId4);
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("33000won"))
        {
            BuyProductID(productId5);
        }
        else if (EventSystem.current.currentSelectedGameObject.name.Equals("5500won"))
        {
            BuyProductID(productId6);
        }
    }
    public void BuyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product p = storeController.products.WithID(productId);

                if (p != null && p.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
                    storeController.InitiatePurchase(p);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }

    public void RestorePurchase()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = extensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions
                (
                    (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
                );
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    {
        Debug.Log("OnInitialized : PASS");

        storeController = sc;
        extensionProvider = ep;
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        switch (args.purchasedProduct.definition.id)
        {
            case productId1:

                // ex) gem 10개 지급
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 10);
                PlayerPrefs.Save();
                


                break;

            case productId2:

                // ex) gem 50개 지급
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 31);
                PlayerPrefs.Save();

                break;

            case productId3:

                // ex) gem 100개 지급
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 53);
                PlayerPrefs.Save();

                break;

            case productId4:

                // ex) gem 300개 지급
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 108);
                PlayerPrefs.Save();

                break;

            case productId5:

                // ex) gem 500개 지급
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 313);
                PlayerPrefs.Save();

                break;
            case productId6:

                // ex) gem 500개 지급
                PlayerPrefs.SetInt("Gem", PlayerPrefs.GetInt("Gem") + 525);
                PlayerPrefs.Save();
                break;

        }
        GemText.text = "" + PlayerPrefs.GetInt("Gem");

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }


   
}
