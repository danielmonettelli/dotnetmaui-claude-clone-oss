using System;
using System.Threading.Tasks;
using Claude.Services;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Claude.Models;

namespace Claude.Services
{
    /// <summary>
    /// Provides a simulation of Anthropic API responses when the actual API is unavailable.
    /// Implements IAnthropicService interface to be a drop-in replacement.
    /// </summary>
    public class SimulatedAnthropicService : IAnthropicService
    {
        // Random for variety in responses
        private readonly Random _random = new Random();
        
        // Historial de mensajes para simular coherencia
        private readonly List<string> _chatHistory = new List<string>();
        
        // List of predefined responses
        private readonly List<string> _simulatedResponses = new List<string>
        {
            "¡Hola! Soy Claude en modo simulación. Estoy aquí para ayudarte aunque actualmente no estoy conectado a la API real.",
            
            "Gracias por tu mensaje. Esta es una respuesta simulada porque la API no está disponible en este momento. La interfaz de usuario sigue funcionando normalmente.",
            
            "Estoy funcionando en modo simulación para que puedas probar la interfaz de usuario sin necesidad de conectarte a la API real. ¿Qué te parece el diseño?",
            
            "Como estamos en modo simulación, mis respuestas son predefinidas. En modo normal, respondería a tu pregunta específica usando la API de Anthropic.",
            
            "La función de simulación te permite trabajar en la interfaz de usuario sin preocuparte por los límites de la API o la conexión a Internet. ¿Hay algún aspecto de la UI que quieras mejorar?",
            
            "Este modo de demostración es útil para verificar animaciones, diseño y experiencia de usuario sin consumir tokens de la API. ¿Te gusta cómo se ve la interfaz?",
            
            "En este modo simulado estoy proporcionando respuestas pregrabadas. Cuando la API esté disponible, podré responder de forma más personalizada."
        };

        // Responses for specific keywords
        private readonly Dictionary<string, List<string>> _keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "hola", new List<string> {
                "¡Hola! Soy Claude en modo simulación. ¿En qué puedo ayudarte con la interfaz hoy?",
                "¡Saludos! Estoy funcionando en modo demo. Es un buen momento para probar la UI."
            }},
            { "ayuda", new List<string> {
                "Estoy en modo simulación, pero puedo explicarte que esta aplicación permite conversar con Claude AI cuando está conectada a la API real.",
                "En modo normal, esta aplicación se conecta a la API de Claude para procesar tus preguntas. Ahora mismo estamos usando respuestas simuladas."
            }},
            { "gracias", new List<string> {
                "¡De nada! Me alegra poder ayudar, incluso en modo simulación.",
                "Es un placer. Esta interfaz está diseñada para parecer natural incluso cuando funciona con respuestas predefinidas."
            }},
            { "ui", new List<string> {
                "El modo simulación está diseñado específicamente para trabajar en mejoras de la UI sin depender de la API.",
                "Esta UI simula la experiencia de chat con Claude. Puedes ajustar los estilos, animaciones y comportamiento sin usar la API real."
            }},
            { "diseño", new List<string> {
                "El diseño de esta aplicación imita la experiencia de chat con Claude, con burbujas de mensajes diferenciadas para el usuario y la IA.",
                "Este diseño está optimizado para dispositivos móviles y de escritorio, usando MAUI para conseguir una experiencia fluida en todas las plataformas."
            }},
            { "prueba", new List<string> {
                "Esta es una prueba exitosa del modo simulación. La aplicación está funcionando como se esperaba.",
                "Prueba completada con éxito. El modo simulación está diseñado para facilitar el desarrollo sin depender de la API real."
            }},
            { "demo", new List<string> {
                "Así es, estamos en modo demostración. Puedes probar toda la funcionalidad de la UI sin conectarte a la API.",
                "El modo demo es perfecto para mostrar la aplicación sin preocuparse por los costos de la API o problemas de conexión."
            }},
            { "simulación", new List<string> {
                "En efecto, soy un Claude simulado. Mis respuestas están predeterminadas para facilitar el desarrollo de la UI.",
                "La simulación permite tener la misma experiencia de usuario pero con respuestas generadas localmente en lugar de usar la API."
            }},
            { "tiempo", new List<string> {
                "Incluso en modo simulación, intento responder con diferentes tiempos para simular la latencia real de la API.",
                "Estoy simulando diferentes tiempos de respuesta para que la experiencia sea más realista."
            }}
        };

        // Frases de continuación cuando se detectan ciertas expresiones de contexto
        private readonly Dictionary<string, List<string>> _contextResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "por qué", new List<string> {
                "Es una buena pregunta. En el modo simulado no puedo elaborar respuestas detalladas, pero en modo normal podría investigar esto a fondo.",
                "Esta es una pregunta interesante. La simulación tiene limitaciones, pero la API real podría darte una respuesta más completa."
            }},
            { "cómo", new List<string> {
                "Para hacer eso en un entorno real, Claude usaría su entrenamiento para ofrecerte instrucciones paso a paso.",
                "En modo normal, podría explicarte detalladamente el proceso. Ahora mismo sólo puedo ofrecer esta respuesta simulada."
            }},
            { "qué piensas", new List<string> {
                "Como simulación, no tengo opiniones reales, pero el Claude verdadero analizaría esto con su conocimiento especializado.",
                "En modo normal, Claude podría ofrecer un análisis detallado basado en su entrenamiento."
            }},
            { "puedes", new List<string> {
                "En modo normal, Claude puede realizar esta tarea analizando tu solicitud. Ahora mismo estoy limitado a respuestas simuladas.",
                "Cuando la aplicación está conectada a la API, Claude puede procesar esta solicitud. En simulación solo muestro respuestas predefinidas."
            }}
        };

        // Simulated delay range (ms)
        private readonly int _minDelay = 500;
        private readonly int _maxDelay = 2000;
        
        // Variables para simular la escritura en tiempo real
        private readonly int _charsPerSecond = 20; // Velocidad de "escritura" simulada

        /// <summary>
        /// Simulates sending a chat message and returns a response that appears to be typed in real time.
        /// </summary>
        /// <param name="query">The message content sent by the user</param>
        /// <returns>A simulated response based on the query or a random predefined response</returns>
        public async Task<string> SendChatMessageAsync(string query)
        {
            // Guardar el mensaje en el historial
            _chatHistory.Add(query);
            
            // Simulate initial network delay
            await Task.Delay(_random.Next(_minDelay, _maxDelay));

            // Determinar la respuesta adecuada
            string fullResponse = DetermineResponse(query);
            
            // Simular la escritura en tiempo real no es posible directamente con la interfaz actual
            // pero podemos devolver la respuesta completa y la UI la mostrará de una vez
            return fullResponse;
        }
        
        /// <summary>
        /// Determina la respuesta más apropiada basada en el mensaje del usuario
        /// </summary>
        private string DetermineResponse(string query)
        {
            // Verificar si el mensaje contiene palabras clave
            foreach (var keyword in _keywordResponses.Keys)
            {
                if (query.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    var responses = _keywordResponses[keyword];
                    return responses[_random.Next(responses.Count)];
                }
            }
            
            // Verificar contexto para responder acorde
            foreach (var context in _contextResponses.Keys)
            {
                if (query.Contains(context, StringComparison.OrdinalIgnoreCase))
                {
                    var responses = _contextResponses[context];
                    return responses[_random.Next(responses.Count)];
                }
            }
            
            // Si hay más de 3 mensajes, ocasionalmente hacer referencia a mensajes anteriores
            if (_chatHistory.Count > 3 && _random.Next(100) < 30)
            {
                string previousMessage = _chatHistory[_chatHistory.Count - 2];
                return $"En referencia a tu mensaje anterior sobre '{GetKeyPhrase(previousMessage)}', " +
                       $"este modo simulado intenta mantener cierta coherencia en la conversación, aunque tiene limitaciones.";
            }
            
            // Si no hay coincidencias específicas, devolver una respuesta aleatoria
            return _simulatedResponses[_random.Next(_simulatedResponses.Count)];
        }
        
        /// <summary>
        /// Extrae una frase clave de un mensaje para simular comprensión
        /// </summary>
        private string GetKeyPhrase(string message)
        {
            if (string.IsNullOrWhiteSpace(message) || message.Length < 10)
                return "tu consulta";
                
            // Simplemente tomar una parte del mensaje para simular comprensión
            int startIndex = _random.Next(0, Math.Max(1, message.Length - 10));
            int length = Math.Min(10, message.Length - startIndex);
            
            return message.Substring(startIndex, length).Trim();
        }
    }
}