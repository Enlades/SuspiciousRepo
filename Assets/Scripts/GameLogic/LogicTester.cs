using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZyngaDemo.GameLogic;

public class LogicTester : MonoBehaviour
{
    private GameTileDeck _deck;
    private GameTileGroup _playerHand;
    private GameTileGroup[] _numberArrange, _colorArrange, _smartArrange;

    private void Awake(){
        _deck = new GameTileDeck();

        _playerHand = new GameTileGroup();

        for (int i = 0; i < 14; i++)
        {
            _playerHand.AddGameTile(_deck.GetRandomTile());
        }

        //_playerHand.SortByNumber();

        _colorArrange = new GameTileSameColorArranger(_deck.OkeyTile).Arrange(_playerHand);
        _numberArrange = new GameTileSameNumberArranger(_deck.OkeyTile).Arrange(_playerHand);
        _smartArrange = new GameTileSmartArranger(_deck.OkeyTile).Arrange(_playerHand);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            _deck = new GameTileDeck();

            _playerHand = new GameTileGroup();

            for (int i = 0; i < 14; i++)
            {
                _playerHand.AddGameTile(_deck.GetRandomTile());
            }

            //_playerHand.SortByNumber();

            _colorArrange = new GameTileSameColorArranger(_deck.OkeyTile).Arrange(_playerHand);
            _numberArrange = new GameTileSameNumberArranger(_deck.OkeyTile).Arrange(_playerHand);
            _smartArrange = new GameTileSmartArranger(_deck.OkeyTile).Arrange(_playerHand);
        }
    }

    private void OnGUI(){
        if(_playerHand != null){
            GUILayout.BeginHorizontal();
            for (int i = 0; i < _playerHand.GameTileCount; i++)
            {
                GUILayout.Label(_playerHand[i].ToString());
            }

            GUILayout.Label("|| Okey : " + _deck.OkeyTile.ToString());

            GUILayout.EndHorizontal();

            /*GUILayout.BeginHorizontal();
            _playerHand.SortByColor();
            for (int i = 0; i < _playerHand.GameTileCount; i++)
            {
                GUILayout.Label(_playerHand[i].ToString());
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _playerHand.SortByNumber();
            for (int i = 0; i < _playerHand.GameTileCount; i++)
            {
                GUILayout.Label(_playerHand[i].ToString());
            }
            GUILayout.EndHorizontal();*/
        }

        GUILayout.Label("");

        if (_smartArrange != null)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < _smartArrange.Length; i++)
            {
                GUILayout.BeginVertical();
                for (int j = 0; j < _smartArrange[i].GameTileCount; j++)
                {
                    GUILayout.Label(_smartArrange[i][j].ToString());
                }
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }

        /*if (_colorArrange != null)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < _colorArrange.Length; i++)
            {
                GUILayout.BeginVertical();
                for (int j = 0; j < _colorArrange[i].GameTileCount; j++)
                {
                    GUILayout.Label(_colorArrange[i][j].ToString());
                }
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }

        if (_numberArrange != null)
        {
            GUILayout.BeginHorizontal();
            for (int i = 0; i < _numberArrange.Length; i++)
            {
                GUILayout.BeginVertical();
                for (int j = 0; j < _numberArrange[i].GameTileCount; j++)
                {
                    GUILayout.Label(_numberArrange[i][j].ToString());
                }
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }*/
    }
}
