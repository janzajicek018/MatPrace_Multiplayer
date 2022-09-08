using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Drawing;

public class PlayerController : NetworkBehaviour
{
	[SerializeField] private GameObject pfProjectile;
	[SerializeField] private GameObject projectileSpawner;

	[SerializeField][SyncVar]
	private int playerHealth = 3;
	private float speed = .5f;
	private float maxSpeed = 7f;

	public int PlayerHealth { get { return playerHealth; } set { playerHealth = value; } }
	//https://mirror-networking.gitbook.io/docs/guides/synchronization
	[SyncVar(hook = nameof(OnColorChanged))]
	private UnityEngine.Color playerColor = UnityEngine.Color.white;

	private Rigidbody2D rigidbody;
	private Transform transform;
	private SpriteRenderer render;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		transform = GetComponent<Transform>();
        if (hasAuthority)
        {
			StartCoroutine(Fire());
        }
	}

	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer) return;
		MovePlayer();
		/*if (Input.GetKeyDown(KeyCode.C))
		{
			CmdFireProjectile();
		}*/
	}
	public override void OnStartLocalPlayer()
	{
		/*
        UnityEngine.Color color = new UnityEngine.Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

		CmdSetupPlayer(color);
		*/
	}
	public void MovePlayer()
	{
		if (Input.GetKey(KeyCode.W))
		{
			Vector3 move = Vector3.right * Time.deltaTime * speed;
			rigidbody.AddRelativeForce(move);
		}
		if (rigidbody.velocity.magnitude > maxSpeed)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}
        if (Input.GetKey(KeyCode.A))
		{
            transform.RotateAround(transform.position, Vector3.forward, 180 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
		{
            transform.RotateAround(transform.position, Vector3.back, 180 * Time.deltaTime);
		}
    }

	[Command]
	public void CmdSetupPlayer(UnityEngine.Color color)
    {
		playerColor = color;
    }
	[Command]
	public void CmdFireProjectile()
    {
		RpcFireProjectile();
    }
	[ClientRpc]
	public void RpcFireProjectile()
	{
		GameObject projectile = Instantiate(pfProjectile, projectileSpawner.transform.position, transform.rotation);
		NetworkServer.Spawn(projectile);
		Destroy(projectile, 7);
	}
	IEnumerator Fire()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.C))
            {
				CmdFireProjectile();
				yield return new WaitForSeconds(0.3f);
            }
			yield return null;
        }
    }
	public void TakeDamage(int damage)
    {
		playerHealth -= damage;
		if(playerHealth <= 0)
        {
			gameObject.SetActive(false);
        }
    }
	public void OnColorChanged(UnityEngine.Color oldCol, UnityEngine.Color newCol)
    {
		render = GetComponent<SpriteRenderer>();
		render.color = newCol;
    }
}
