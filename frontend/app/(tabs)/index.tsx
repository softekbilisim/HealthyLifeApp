import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, ScrollView, TouchableOpacity } from 'react-native';
import { useTranslation } from 'react-i18next';
import api from '../../services/api';
import { Colors } from '../../constants/Colors';
import { registerForPushNotificationsAsync, scheduleMotivationNotification } from '../../services/notifications';

export default function DashboardScreen() {
  const { t } = useTranslation();
  const [stats, setStats] = useState<any>(null);

  useEffect(() => {
    fetchStats();
    registerForPushNotificationsAsync();
  }, []);

  const sendMotivation = async () => {
    await scheduleMotivationNotification(
      "Harika Gidiyorsun!",
      "Bugün içmediğin her sigara sana sağlık ve özgürlük katıyor."
    );
  };

  const fetchStats = async () => {
    try {
      const response = await api.get('/smoking/stats');
      setStats(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <ScrollView style={styles.container}>
      <View style={styles.card}>
        <Text style={styles.cardTitle}>{t('moneySaved')}</Text>
        <Text style={styles.cardValue}>{stats?.moneySaved || 0} TL</Text>
      </View>

      <View style={[styles.card, { backgroundColor: Colors.secondary }]}>
        <Text style={styles.cardTitle}>{t('healthImproved')}</Text>
        <Text style={styles.cardValue}>%{stats?.daysPassed || 0} İlerleme</Text>
      </View>

      <View style={styles.infoSection}>
        <Text style={styles.infoText}>İçilmeyen Sigara: {stats?.cigarettesNotSmoked || 0}</Text>
        <Text style={styles.infoText}>Kazanılan Zaman: {stats?.timeRegainedMinutes || 0} dk</Text>
      </View>

      <TouchableOpacity style={styles.motivationButton} onPress={sendMotivation}>
        <Text style={styles.buttonText}>Beni Motive Et!</Text>
      </TouchableOpacity>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, backgroundColor: '#f5f5f5', padding: 20 },
  motivationButton: {
    backgroundColor: Colors.secondary,
    padding: 15,
    borderRadius: 12,
    alignItems: 'center',
    marginTop: 20
  },
  buttonText: { color: 'white', fontWeight: 'bold' },
  card: {
    backgroundColor: Colors.primary,
    padding: 20,
    borderRadius: 15,
    marginBottom: 20,
    elevation: 3,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
  },
  cardTitle: { color: 'white', fontSize: 18, fontWeight: 'bold' },
  cardValue: { color: 'white', fontSize: 32, fontWeight: 'bold', marginTop: 10 },
  infoSection: { padding: 10 },
  infoText: { fontSize: 16, color: Colors.text, marginBottom: 5 }
});
