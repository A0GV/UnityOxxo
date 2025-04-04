using UnityEngine;
using UnityEngine.UI;

public class TextLogicsAndResults : MonoBehaviour
{
    public Text resumen;

     static string[] informes = {
        "Gestión de Líderes\n3/5 líderes mejoraron sus tiendas.\n2 líderes fallaron por no implementar mejoras ni resolver problemas clave.\nRecomendación: Evalúa mejor su experiencia antes de aprobarlos.\n\nImpacto en Ventas\n+1,200 ventas por promociones exitosas.\n-800 ventas por inventario insuficiente.\nRecomendación: Refuerza el control de inventario en tiendas con alta demanda.",
        "Gestión de Líderes\n4/6 líderes mejoraron sus tiendas implementando nuevas estrategias de ventas.\n2 líderes fallaron debido a la falta de resolución de problemas clave.\nRecomendación: Proveer capacitaciones adicionales antes de asignar nuevos líderes.\n\nImpacto en Ventas\n+1,500 ventas por nuevas promociones.\n-900 ventas por falta de stock.\nRecomendación: Mejora la gestión de inventarios en tiendas con alta demanda.",
        "Gestión de Líderes\n5/7 líderes lograron mejorar significativamente el rendimiento de sus tiendas.\n2 líderes no alcanzaron los objetivos debido a la falta de implementación de mejoras.\nRecomendación: Realizar evaluaciones más exhaustivas antes de aprobar nuevos líderes.\n\nImpacto en Ventas\n+1,300 ventas añadidas gracias a estrategias de ventas innovadoras.\n-900 ventas perdidas por falta de productos en stock durante promociones clave.\nRecomendación: Fortalecer la cadena de suministro y el control de inventarios.",
        "Gestión de Líderes\n3/5 líderes implementaron con éxito nuevas medidas para mejorar la eficiencia.\n2 líderes no pudieron resolver problemas críticos, afectando el desempeño.\nRecomendación: Revisar las habilidades de resolución de problemas antes de la aprobación.\n\nImpacto en Ventas\n+1,800 ventas incrementadas por nuevas tácticas de marketing.\n-850 ventas reducidas debido a la falta de recursos en almacén.\nRecomendación: Optimizar la gestión del inventario y prever la demanda con mayor precisión.",
        "Gestión de Líderes\n6/8 líderes mejoraron la productividad en sus tiendas.\n2 líderes fallaron por no adoptar nuevas prácticas recomendadas.\nRecomendación: Asegurarse de que los líderes comprendan y adopten las mejores prácticas antes de su designación.\n\nImpacto en Ventas\n+2,000 ventas por campañas publicitarias efectivas.\n-1,000 ventas por problemas de suministro y logística.\nRecomendación: Implementar soluciones para mejorar la logística y el abastecimiento de productos."
    };

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, informes.Length);
        resumen.text = informes[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
