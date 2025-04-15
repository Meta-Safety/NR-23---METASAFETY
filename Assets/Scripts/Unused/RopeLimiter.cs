using UnityEngine;
using Obi;

public class RopeLimiter : MonoBehaviour
{
    public GameObject attachment;   // Referência ao attachment da corda
    public GameObject extinguisher; // Referência ao extintor como ponto de referência



    // Limites de deslocamento em relação ao extintor
    public Vector2 xLimits = new Vector2(-0.4f, 0.5f); // Limites no eixo X
    public Vector2 yLimits = new Vector2(-1.5f, 0.0f); // Limites no eixo Y
    public Vector2 zLimits = new Vector2(-1.0f, 1.0f); // Limites no eixo Z


    void LateUpdate()
    {
        // Obtenha a posição atual do attachment e do extintor
        Vector3 attachmentPosition = attachment.transform.position;
        Vector3 extinguisherPosition = extinguisher.transform.position;

        // Calcule a posição relativa do attachment em relação ao extintor
        Vector3 relativePosition = attachmentPosition - extinguisherPosition;

        // Limite a posição relativa no eixo X
        relativePosition.x = Mathf.Clamp(relativePosition.x, xLimits.x, xLimits.y);

        // Limite a posição relativa no eixo Y
        relativePosition.y = Mathf.Clamp(relativePosition.y, yLimits.x, yLimits.y);

        // Limite a posição relativa no eixo Z
        relativePosition.z = Mathf.Clamp(relativePosition.z, zLimits.x, zLimits.y);

        // Converta a posição limitada de volta para a posição global
        attachment.transform.position = extinguisherPosition + relativePosition;
    }
}
