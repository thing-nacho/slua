export PATH=/Users/macbuild/work/android-ndk-r11b:$PATH


cd android/jni
./build_arm_v7.sh
./build_x86.sh
cd ../../

