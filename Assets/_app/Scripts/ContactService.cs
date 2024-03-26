using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactService
{
    DB db;

    public ContactService(){
        db = new DB();
    }

    public void CreateContactTableDB(){
		db.GetConnection().DropTable<Card> ();
		db.GetConnection().CreateTable<Card> ();
    }
}
