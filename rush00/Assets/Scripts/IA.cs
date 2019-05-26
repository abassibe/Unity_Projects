using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : Character
{
    bool isTriggered = false;
    CircleCollider2D pov;
    PolygonCollider2D areaPov;

    public Character character;

    public Room[] rooms;

    public door[] doors;
    public door previewDoor = null;

    public List<Gun> guns = new List<Gun>();

    Vector2 firstPointDoor;
    Vector2 secondPointDoor;
    public bool doorPassed = true;
    public bool firstPointPassed = false;

    void Start()
    {
        pov = GetComponent<CircleCollider2D>();
        areaPov = GetComponent<PolygonCollider2D>();
        gun = Instantiate(pick<Gun>(guns));
        gun.owner = gameObject;
        getGun();
    }

    void Update()
    {
        if (character && isTriggered && HP > 0)
        {
            Vector2 direction = (character.gameObject.transform.position - transform.position).normalized;
            Vector2 origin = (Vector2)transform.position + direction;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, /*gun.range*/10);

            Vector2 targetDir = hit.point - (Vector2)transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            Debug.DrawRay(origin, direction, Color.black);
            if (characterIsOnView())
                gun.shoot(direction);
            else
                findPOV();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Vector2 direction = (character.gameObject.transform.position - transform.position).normalized;
            Vector2 origin = (Vector2)transform.position + direction;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, /*gun.range*/10);
            Debug.DrawRay(origin, direction, Color.black);
            if (hit.collider.name == "Character")
                isTriggered = true;
        }
    }

    bool characterIsOnView()
    {
        Vector2 direction = (character.gameObject.transform.position - transform.position).normalized;
        Vector2 origin = (Vector2)transform.position + direction;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, /*gun.range*/10);
        Debug.DrawRay(origin, direction, Color.black);
        if (hit && hit.collider.name == "Character")
            return true;
        return false;
    }

    void findPOV()
    {
        Room targetRoom = null;
        Room currentRoom = null;
        door targetDoor = null;
        foreach (Room room in rooms)
        {
            if ((character.transform.position.x >= room.xLeft && character.transform.position.x <= room.xRigth) &&
                (character.transform.position.y >= room.yLeft && character.transform.position.y <= room.yRigth))
                targetRoom = room;
            if ((transform.position.x >= room.xLeft && transform.position.x <= room.xRigth) &&
                (transform.position.y >= room.yLeft && transform.position.y <= room.yRigth))
                currentRoom = room;

            if (targetRoom && currentRoom)
                break;
        }
        if (targetRoom == currentRoom)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, step);
            doorPassed = true;
            firstPointPassed = true;
        }
        else if (doorPassed && firstPointPassed)
        {
            foreach (door d in doors)
            {
                if ((d.connectedRoom[0] == currentRoom && d.connectedRoom[1] == targetRoom) || (d.connectedRoom[1] == currentRoom && d.connectedRoom[0] == targetRoom))
                {
                    targetDoor = d;
                    break;
                }
                else if (d.connectedRoom[0] != previewDoor && d.connectedRoom[0] == currentRoom)
                {
                    if (targetDoor == null)
                        targetDoor = d;
                    else
                    {
                        float newDoor = (d.connectedRoom[0].transform.position.x + d.connectedRoom[0].transform.position.y) - (character.transform.position.x + character.transform.position.y);
                        float lastTargetDoor = (targetDoor.connectedRoom[0].transform.position.x + targetDoor.connectedRoom[0].transform.position.y) - (character.transform.position.x + character.transform.position.y);
                        if (lastTargetDoor < newDoor)
                            targetDoor = d;
                    }
                }
                else if (d.connectedRoom[1] != previewDoor && d.connectedRoom[1] == currentRoom)
                {
                    if (targetDoor == null)
                        targetDoor = d;
                    else
                    {
                        float newDoor = (d.connectedRoom[0].transform.position.x + d.connectedRoom[0].transform.position.y) - (character.transform.position.x + character.transform.position.y);
                        float lastTargetDoor = (targetDoor.connectedRoom[0].transform.position.x + targetDoor.connectedRoom[0].transform.position.y) - (character.transform.position.x + character.transform.position.y);
                        if (lastTargetDoor < newDoor)
                            targetDoor = d;
                    }
                }
            }
            previewDoor = targetDoor;
            float step = speed * Time.deltaTime;
            float firstPoint = (transform.position.x + transform.position.y) - (targetDoor.point1.x + targetDoor.point1.y);
            float secondPoint = (transform.position.x + transform.position.y) - (targetDoor.point2.x + targetDoor.point2.y);
            if (firstPoint < secondPoint)
            {
                firstPointDoor = targetDoor.point1;
                secondPointDoor = targetDoor.point2;
            }
            else
            {
                firstPointDoor = targetDoor.point2;
                secondPointDoor = targetDoor.point1;
            }
            doorPassed = false;
            firstPointPassed = false;
            transform.position = Vector3.MoveTowards(transform.position, firstPointDoor, step);
        }
        else if (!firstPointPassed)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, firstPointDoor, step);
            if ((Vector2)transform.position == firstPointDoor)
                firstPointPassed = true;
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, secondPointDoor, step);
            if ((Vector2)transform.position == secondPointDoor)
                doorPassed = true;
        }
    }
}