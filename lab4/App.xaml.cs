using Lab_4.Classes;
using Lab_4.DTOs;
using System.Configuration;
using System.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Windows;
using System.IO;
using System.Linq;

namespace lab4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string DataFilePath = "app_data.json";

        private static readonly JsonSerializerOptions _serializeOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        private static readonly JsonSerializerOptions _deserializeOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }
        };

        public static void SaveYouthCenterData(YouthCreativityCenter center)
        {
            try
            {
                YouthCreativityCenterDTO centerDto = center.ToDTO();
                string jsonString = JsonSerializer.Serialize(centerDto, _serializeOptions);
                File.WriteAllText(DataFilePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при збереженні даних: {ex.Message}", "Помилка збереження", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static YouthCreativityCenter LoadYouthCenterData()
        {
            YouthCreativityCenter loadedCenter = null;

            if (File.Exists(DataFilePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(DataFilePath);

                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        YouthCreativityCenterDTO centerDto = JsonSerializer.Deserialize<YouthCreativityCenterDTO>(jsonString, _deserializeOptions);
                        if (centerDto != null)
                        {
                            loadedCenter = YouthCreativityCenter.FromDTO(centerDto);
                            return loadedCenter;
                        }
                    }
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"Помилка десеріалізації даних: {ex.Message}. Буде створено новий список гуртків.", "Помилка завантаження", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при завантаженні даних: {ex.Message}. Буде створено новий список гуртків.", "Помилка завантаження", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show($"Файл даних '{DataFilePath}' не знайдено. Буде створено новий список гуртків.", "Завантаження", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            return new YouthCreativityCenter("Адреса не вказана");
        }
    }

}
