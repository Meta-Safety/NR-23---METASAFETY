using UnityEngine;
using Obi;

public class RopeLimiter : MonoBehaviour
{
    public GameObject attachment;   // Refer�ncia ao attachment da corda
    public GameObject extinguisher; // Refer�ncia ao extintor como ponto de refer�ncia



    // Limites de deslocamento em rela��o ao extintor
    public Vector2 xLimits = new Vector2(-0.4f, 0.5f); // Limites no eixo X
    public Vector2 yLimits = new Vector2(-1.5f, 0.0f); // Limites no eixo Y
    public Vector2 zLimits = new Vector2(-1.0f, 1.0f); // Limites no eixo Z


    void LateUpdate()
    {
        // Obtenha a posi��o atual do attachment e do extintor
        Vector3 attachmentPosition = attachment.transform.position;
        Vector3 extinguisherPosition = extinguisher.transform.position;

        // Calcule a posi��o relativa do attachment em rela��o ao extintor
        Vector3 relativePosition = attachmentPosition - extinguisherPosition;

        // Limite a posi��o relativa no eixo X
        relativePosition.x = Mathf.Clamp(relativePosition.x, xLimits.x, xLimits.y);

        // Limite a posi��o relativa no eixo Y
        relativePosition.y = Mathf.Clamp(relativePosition.y, yLimits.x, yLimits.y);

        // Limite a posi��o relativa no eixo Z
        relativePosition.z = Mathf.Clamp(relativePosition.z, zLimits.x, zLimits.y);

        // Converta a posi��o limitada de volta para a posi��o global
        attachment.transform.position = extinguisherPosition + relativePosition;
    }
}
