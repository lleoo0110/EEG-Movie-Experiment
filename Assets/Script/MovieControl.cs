using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class MovieControl : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    public static MovieControl instance;
    private UdpClient udpClient;
    private string ipAddress = "127.0.0.1";
    private int port = 12345;
    private bool isMovieStarted = false; // 動画が開始されたかどうかを追跡する変数

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        udpClient = new UdpClient();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isMovieStarted)
        {
            StartMovie();
        }
    }

    private void StartMovie()
    {
        Debug.Log("Movie Start");
        videoPlayer.Play();
        SendData("MovieStart");
        isMovieStarted = true; // 動画が開始されたことをマーク
    }

    public void SendData(string data) 
    {
        byte[] sendData = Encoding.UTF8.GetBytes(data);
        udpClient.Send(sendData, sendData.Length, ipAddress, port);
    }
}