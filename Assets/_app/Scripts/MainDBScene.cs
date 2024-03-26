using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBScene : MonoBehaviour
{
    ContactService contactService;
    // Start is called before the first frame update
    void Start()
    {
        contactService = new ContactService();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCreateContactTableButtonClick(){
        contactService.CreateContactTableDB();
    }
}
