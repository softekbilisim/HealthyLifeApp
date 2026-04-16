import React, { useState } from 'react';
import { View, Text, StyleSheet, TextInput, TouchableOpacity, ScrollView } from 'react-native';
import { useTranslation } from 'react-i18next';
import api from '../../services/api';
import { Colors } from '../../constants/Colors';

export default function NutritionScreen() {
  const { t } = useTranslation();
  const [profile, setProfile] = useState({
    weight: '',
    height: '',
    age: '',
    bloodType: '',
    gender: 'male'
  });
  const [result, setResult] = useState<any>(null);

  const handleSave = async () => {
    try {
      const response = await api.post('/nutrition/profile', {
        ...profile,
        weight: parseFloat(profile.weight),
        height: parseFloat(profile.height),
        age: parseInt(profile.age)
      });
      setResult(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <ScrollView style={styles.container}>
      <Text style={styles.title}>{t('nutrition')}</Text>

      <TextInput
        style={styles.input}
        placeholder={t('weight')}
        value={profile.weight}
        onChangeText={(text) => setProfile({...profile, weight: text})}
        keyboardType="numeric"
      />
      <TextInput
        style={styles.input}
        placeholder="Boy (cm)"
        value={profile.height}
        onChangeText={(text) => setProfile({...profile, height: text})}
        keyboardType="numeric"
      />
      <TextInput
        style={styles.input}
        placeholder={t('bloodType')}
        value={profile.bloodType}
        onChangeText={(text) => setProfile({...profile, bloodType: text})}
      />

      <TouchableOpacity style={styles.button} onPress={handleSave}>
        <Text style={styles.buttonText}>{t('save')}</Text>
      </TouchableOpacity>

      {result && (
        <View style={styles.resultCard}>
          <Text style={styles.resultText}>{t('calorieNeed')}: {Math.round(result.dailyCalorieNeed)} kcal</Text>
          <Text style={styles.subText}>Kan Grubunuza göre öneriler listeleniyor...</Text>
        </View>
      )}
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20, backgroundColor: 'white' },
  title: { fontSize: 24, fontWeight: 'bold', color: Colors.primary, marginBottom: 20 },
  input: {
    borderWidth: 1,
    borderColor: Colors.gray,
    borderRadius: 8,
    padding: 12,
    marginBottom: 15,
  },
  button: {
    backgroundColor: Colors.primary,
    padding: 15,
    borderRadius: 8,
    alignItems: 'center',
  },
  buttonText: { color: 'white', fontWeight: 'bold', fontSize: 16 },
  resultCard: {
    marginTop: 30,
    padding: 20,
    backgroundColor: '#f0fff4',
    borderRadius: 12,
    borderWidth: 1,
    borderColor: Colors.primary,
  },
  resultText: { fontSize: 20, fontWeight: 'bold', color: Colors.primary },
  subText: { marginTop: 10, color: Colors.text }
});
