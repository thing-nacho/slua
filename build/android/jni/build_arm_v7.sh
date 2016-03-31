rm ./libluajit.a
echo APP_ABI := armeabi-v7a>./Application.mk

cd luajit/src
NDK=/Users/macbuild/work/android-ndk-r11b
NFK=$NDK/toolchains/arm-linux-androideabi-4.9/prebuilt/darwin-x86_64/bin/arm-linux-androideabi-
make clean

make HOST_CC="gcc -m32 -ffast-math -O3" \
CROSS=$NFK \
TARGET_SYS=Linux \
TARGET_FLAGS="--sysroot $NDK/platforms/android-14/arch-arm -march=armv7-a -Wl,--fix-cortex-a8"
cp ./libluajit.a ../../libluajit.a
cd ../../../
ndk-build clean
ndk-build
