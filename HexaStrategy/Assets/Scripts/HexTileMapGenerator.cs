using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileMapGenerator : MonoBehaviour
{
    private float amp = 10f;
    private float freq = 10f;

    public GameObject[] CurrentHex;
    public GameObject Dirt;
    private GameObject TempGO;

    public float tileOffsetX = 1.8f;
    public float tileOffsetZ = 1.565f;

    public int mapWidth;
    public int mapHeight;

    void Start()
    {
        CreateHexMap();
    }

    private void Update()
    {
        //�jra gener�lunk, de el�bb az el�z�t t�r�lj�k
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyMap();
            CreateHexMap();
        }
    }

    void CreateHexMap()
    {
        //l�nyeg�ben ez a seed
        int randomizer = Random.Range(0, 10000);

        //v�gig szaladunk a m�trixon
        for (int x = -mapWidth/2; x <= mapWidth/2; x++)
        {
            for(int z = -mapHeight/2; z <= mapHeight/2; z++)
            {
                //hozz�adjuk a perlinzajhoz a seed-et �s megs�zzuk a magass�ggal
                float y = Mathf.PerlinNoise((x + randomizer) / freq, (z + randomizer) / freq) * amp;
                
                //kerek�tj�k az y-t
                y = Mathf.Floor(y);

                //V�ltozatosabb elemek a magass�gt�l f�gg�en
                if(y > amp / 2)
                {
                    TempGO = Instantiate(CurrentHex[0]);
                }
                else
                {
                    TempGO = Instantiate(CurrentHex[1]);
                }
                
                //minden m�sodik sort el kell tolni kicsit
                if (z % 2 == 0)
                {
                    TempGO.transform.position = new Vector3(x * tileOffsetX, y, z * tileOffsetZ);
                }
                else
                {
                    TempGO.transform.position = new Vector3(x * tileOffsetX + tileOffsetX/2, y, z * tileOffsetZ);
                }

                //ha egy elem magasabban van mint a minimum, akkor az alatta l�v� helyet kit�ltj�k
                if(y > 1)
                {
                    
                    for(int i = (int)y-1; i >= 1; i--)
                    {
                        GameObject dirtGO = Instantiate(Dirt);
                        dirtGO.transform.position = new Vector3(TempGO.transform.position.x, i, TempGO.transform.position.z);
                        StartCoroutine(SetTileInfo(dirtGO, x, z, true));
                    }
                    
                    // er�forr�s sp�rol�s miatt csak egy helyet t�lt�nk ki alatta
                    /*
                    GameObject dirtGO = Instantiate(Dirt);
                    dirtGO.transform.position = new Vector3(TempGO.transform.position.x, y - 1, TempGO.transform.position.z);
                    StartCoroutine(SetTileInfo(dirtGO, x, z, true));
                    */
                }

                StartCoroutine(SetTileInfo(TempGO, x, z, false));
            }
        }
    }

    //a gyermek elemeket elnevezz�k �s elrendezz�k
    IEnumerator SetTileInfo(GameObject GO, int x, int z, bool isBase)
    {
        yield return new WaitForSeconds(0.0001f);

        GO.transform.parent = transform;
        if(isBase)
        {
            GO.name = "Base";
        }
        else
        {
            GO.name = x.ToString() + ", " + z.ToString();
        }
        
    }

    //t�r�lj�k az el�z� map-et az �j gener�l�sa el�tt
    void DestroyMap()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
