// Copyright 2021, Infima Games. All Rights Reserved.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Interface Element.
    /// </summary>
    public abstract class Element : MonoBehaviour
    {
        #region FIELDS
        
        /// <summary>
        /// Game Mode Service.
        /// </summary>
        protected IGameModeService gameModeService;
        
        /// <summary>
        /// Player Character.
        /// </summary>
        protected CharacterBehaviour playerCharacter;
        /// <summary>
        /// Player Character Inventory.
        /// </summary>
        protected InventoryBehaviour playerCharacterInventory;

        /// <summary>
        /// Equipped Weapon.
        /// </summary>
        protected WeaponBehaviour equippedWeapon;
        // private pause_menu_script pms;
        
        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        protected virtual void Awake()
        {
            //Get Game Mode Service. Very useful to get Game Mode references.
            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            
            //Get Player Character.
            playerCharacter = gameModeService.GetPlayerCharacter();
            //Get Player Character Inventory.
            playerCharacterInventory = playerCharacter.GetInventory();
        }
        
        /// <summary>
        /// Update.
        /// </summary>
        private void Update()
        {
            //Ignore if we don't have an Inventory.
            if (Equals(playerCharacterInventory, null))
                return;

            //Get Equipped Weapon.
            equippedWeapon = playerCharacterInventory.GetEquipped();
            
            //Tick.
            Tick();
            // if(pms.gamepaused){
            //       if(Input.GetKeyDown(KeyCode.Backspace)){
            //          Time.timeScale=1f;
            //         SceneManager.LoadScene("game");
            //     }
            // }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Tick.
        /// </summary>
        // protected virtual void pause_menu_script() {}
        protected virtual void Tick() {}

        #endregion
    }
}