import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

const resources = {
  tr: {
    translation: {
      welcome: 'Hoş Geldiniz',
      smoking: 'Sigara Takibi',
      nutrition: 'Beslenme',
      sugar: 'Şeker Bırakma',
      settings: 'Ayarlar',
      save: 'Kaydet',
      dailyCount: 'Günlük Sigara Sayısı',
      minutesPerCigarette: 'Bir Sigara Kaç Dakika?',
      packPrice: 'Paket Fiyatı',
      quitDate: 'Bırakma Tarihi',
      moneySaved: 'Tasarruf Edilen Para',
      healthImproved: 'Sağlık İlerlemesi',
      bloodType: 'Kan Grubu',
      weight: 'Kilo',
      calorieNeed: 'Günlük Kalori İhtiyacı'
    }
  },
  en: {
    translation: {
      welcome: 'Welcome',
      smoking: 'Smoking Tracker',
      nutrition: 'Nutrition',
      sugar: 'Sugar Free',
      settings: 'Settings',
      save: 'Save',
      dailyCount: 'Daily Cigarette Count',
      minutesPerCigarette: 'Minutes Per Cigarette',
      packPrice: 'Pack Price',
      quitDate: 'Quit Date',
      moneySaved: 'Money Saved',
      healthImproved: 'Health Improvement',
      bloodType: 'Blood Type',
      weight: 'Weight',
      calorieNeed: 'Daily Calorie Need'
    }
  }
};

i18n
  .use(initReactI18next)
  .init({
    resources,
    lng: 'tr',
    interpolation: {
      escapeValue: false
    }
  });

export default i18n;
