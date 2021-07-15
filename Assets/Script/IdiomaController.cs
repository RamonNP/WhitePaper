using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdiomaController : MonoBehaviour
{
    
    public TextMeshProUGUI[] textMeshProList;
    public GameObject[] npcRecarregarIdioma;
    public Dictionary<string, Dialogo> dialogosEng = new Dictionary<string, Dialogo>();
    public Dictionary<string, Dialogo> dialogosBr = new Dictionary<string, Dialogo>();
    public TextAsset jsonFileEng;
    public TextAsset jsonFileBr;
    public static IdiomaController instance;
    public string idiomaAtual;

    private void Awake() { 
        if (instance == null) 
		{
			instance = this;
        }
        LoadItensDatabase();
        MudarIdioma("ENG");
        idiomaAtual = "ENG";
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Application.ExternalCall("OnUnityReady");
    }
    public void SetLanguage(string _langID)
     {
        print("LINGU*AGEM EXTERNA DO SITE"+_langID);
        MudarIdioma(_langID);
        idiomaAtual = _langID;
     }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadItensDatabase() {
		ListaItensJson itensJson = JsonUtility.FromJson<ListaItensJson>(jsonFileEng.text);
		foreach (Dialogo item in itensJson.Itens)
        {
			dialogosEng.Add(item.chave, item);
        }
		ListaItensJson itensJsonBr = JsonUtility.FromJson<ListaItensJson>(jsonFileBr.text);
		foreach (Dialogo item in itensJsonBr.Itens)
        {
			dialogosBr.Add(item.chave, item);
        }
	}
    
    public void MudarIdioma(string idioma) {
        //LER EM TEMPO, Newtom
        idiomaAtual = idioma;
        if(idioma == "pt-BR" || idioma == "pt") {
            Dialogo dialogo;
            foreach (TextMeshProUGUI item in textMeshProList)
            {
                dialogosBr.TryGetValue(item.gameObject.name, out dialogo);
                item.text = dialogo.valor;
            }
        } else  {
            Dialogo dialogo;
            foreach (TextMeshProUGUI item in textMeshProList)
            {
                dialogosEng.TryGetValue(item.gameObject.name, out dialogo);
                item.text = dialogo.valor;
            }
        }
        foreach (GameObject item in npcRecarregarIdioma)
        {
            item.GetComponent<NpcSentence>().inicializaDialogo();
        }
    }
    
}
