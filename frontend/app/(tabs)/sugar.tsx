import React from 'react';
import { View, Text, StyleSheet, FlatList } from 'react-native';
import { useTranslation } from 'react-i18next';
import { Colors } from '../constants/Colors';

const MOCK_RECIPES = [
  { id: '1', title: 'Hurmalı Toplar', desc: 'Şeker krizleri için birebir sağlıklı atıştırmalık.' },
  { id: '2', title: 'Tarçınlı Çay', desc: 'Kan şekerini dengelemeye yardımcı olur.' }
];

export default function SugarScreen() {
  const { t } = useTranslation();

  return (
    <View style={styles.container}>
      <Text style={styles.title}>{t('sugar')}</Text>

      <View style={styles.timeline}>
        <Text style={styles.timelineTitle}>Şekersiz Hayat Yolculuğu</Text>
        <Text style={styles.timelineItem}>• 1. Gün: Tat alma duyusu iyileşmeye başlar.</Text>
        <Text style={styles.timelineItem}>• 7. Gün: Ciltte parlama ve enerji artışı.</Text>
      </View>

      <Text style={[styles.title, { marginTop: 20 }]}>Alternatif Tarifler</Text>
      <FlatList
        data={MOCK_RECIPES}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
          <View style={styles.recipeCard}>
            <Text style={styles.recipeTitle}>{item.title}</Text>
            <Text style={styles.recipeDesc}>{item.desc}</Text>
          </View>
        )}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20, backgroundColor: 'white' },
  title: { fontSize: 24, fontWeight: 'bold', color: '#8E44AD', marginBottom: 20 },
  timeline: {
    padding: 15,
    backgroundColor: '#F4ECF7',
    borderRadius: 12,
  },
  timelineTitle: { fontWeight: 'bold', marginBottom: 10, color: '#6C3483' },
  timelineItem: { marginBottom: 5, color: Colors.text },
  recipeCard: {
    padding: 15,
    backgroundColor: 'white',
    borderRadius: 8,
    borderWidth: 1,
    borderColor: '#D7BDE2',
    marginBottom: 10,
  },
  recipeTitle: { fontWeight: 'bold', color: '#8E44AD' },
  recipeDesc: { color: Colors.text, fontSize: 13, marginTop: 4 }
});
