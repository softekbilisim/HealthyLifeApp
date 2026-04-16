import React, { useState } from 'react';
import { View, Text, StyleSheet, TextInput, TouchableOpacity, Alert } from 'react-native';
import { useRouter } from 'expo-router';
import api from '../services/api';
import { Colors } from '../constants/Colors';

export default function RegisterScreen() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [fullName, setFullName] = useState('');
  const router = useRouter();

  const handleRegister = async () => {
    try {
      await api.post('/identity/register', { email, password, fullName });
      Alert.alert('Başarılı', 'Kayıt olundu. Giriş yapabilirsiniz.');
      router.push('/login');
    } catch (error) {
      Alert.alert('Hata', 'Kayıt yapılamadı.');
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Yeni Kayıt</Text>
      <TextInput
        style={styles.input}
        placeholder="Ad Soyad"
        value={fullName}
        onChangeText={setFullName}
      />
      <TextInput
        style={styles.input}
        placeholder="E-posta"
        value={email}
        onChangeText={setEmail}
        autoCapitalize="none"
      />
      <TextInput
        style={styles.input}
        placeholder="Şifre"
        value={password}
        onChangeText={setPassword}
        secureTextEntry
      />
      <TouchableOpacity style={styles.button} onPress={handleRegister}>
        <Text style={styles.buttonText}>Kayıt Ol</Text>
      </TouchableOpacity>
      <TouchableOpacity onPress={() => router.back()}>
        <Text style={styles.linkText}>Zaten hesabınız var mı? Giriş yapın</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: 'center', padding: 20, backgroundColor: 'white' },
  title: { fontSize: 28, fontWeight: 'bold', color: Colors.primary, textAlign: 'center', marginBottom: 30 },
  input: { borderWidth: 1, borderColor: Colors.gray, borderRadius: 8, padding: 12, marginBottom: 15 },
  button: { backgroundColor: Colors.primary, padding: 15, borderRadius: 8, alignItems: 'center', marginBottom: 20 },
  buttonText: { color: 'white', fontWeight: 'bold', fontSize: 16 },
  linkText: { color: Colors.secondary, textAlign: 'center' }
});
